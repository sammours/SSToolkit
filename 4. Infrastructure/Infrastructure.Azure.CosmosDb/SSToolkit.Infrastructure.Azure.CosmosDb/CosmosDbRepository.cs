namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Fundamental;
    using SSToolkit.Fundamental.Extensions;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Extensions;

    public class CosmosDbRepository<TEntity> : ICosmosDbRepository<TEntity>
        where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
    {
        private Container container;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // Container will be initialized in InitializeMethod
        public CosmosDbRepository(CosmosDbRepositoryOptions<TEntity> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Options = options;
            this.CosmosDbLinqQuery = new CosmosDbLinqQuery();
            this.Initialize();
        }

        // For testing purposes
        protected virtual ICosmosDbLinqQuery CosmosDbLinqQuery { get; set; }

        protected CosmosDbRepositoryOptions<TEntity> Options { get; }

        public async Task<IEnumerable<TEntity>> FindAllAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.FindAllAsync(Enumerable.Empty<ISpecification<TEntity>>(), options, cancellationToken: cancellationToken).AnyContext();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.FindAllAsync(new[] { specification }, options, cancellationToken: cancellationToken).AnyContext();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var specificationsArray = specifications as ISpecification<TEntity>[] ?? specifications.ToArray();
            var expressions = specificationsArray.Safe().Select(s => s.ToExpression());
            var order = options?.GetOrders().FirstOrDefault();

            var requestOptions = this.GetRequestOptions();
            double requestCharge = 0;
            var result = new List<TEntity>();
            var query = this.container.GetItemLinqQueryable<TEntity>(requestOptions: requestOptions)
                .WhereIf(expressions);
            var query1 = query
                .OrderByIf(order?.Expression, order?.Direction == OrderByDirection.Desc);
            var query2 = query1
                .SkipIf(options?.Skip);
            var query3 = query2
                .TakeIf(options?.Take);

            var iterator = this.CosmosDbLinqQuery.GetFeedIterator(query);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken: cancellationToken).AnyContext();
                requestCharge += response.RequestCharge;
                foreach (var entity in response.Resource)
                {
                    result.Add(entity);
                }
            }

            return result.ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(string query, List<DbParameter> dbParameters, CancellationToken cancellationToken = default)
        {
            double requestCharge = 0;
            var result = new List<TEntity>();
            string? continuationToken = null;

            var queryDefinition = new QueryDefinition(query);
            foreach (var dbParameter in dbParameters)
            {
                queryDefinition.WithParameter($"@{dbParameter.Field}", dbParameter.Value);
            }

            var requestOptions = this.GetRequestOptions();
            var iterator = this.container.GetItemQueryIterator<TEntity>(queryDefinition: queryDefinition, continuationToken: continuationToken, requestOptions: requestOptions);
            while (iterator.HasMoreResults)
            {
                FeedResponse<TEntity> response = await iterator.ReadNextAsync(cancellationToken: cancellationToken).AnyContext();
                continuationToken = response.ContinuationToken;

                requestCharge += response.RequestCharge;
                foreach (var entity in response.Resource)
                {
                    result.Add(entity);
                }
            }

            return result.ToList();
        }

        public async Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.IsDefault())
            {
                return default;
            }

            var requestOptions = this.GetRequestOptions();
            var sqlQuery = new QueryDefinition($"select * from c where c.id = @id").WithParameter("@id", id.ToString());
            var iterator = this.container.GetItemQueryIterator<TEntity>(
                sqlQuery,
                requestOptions: requestOptions);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken: cancellationToken).AnyContext();
                foreach (var result in response.Resource)
                {
                    return result;
                }
            }

            return default;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.IsDefault())
            {
                return false;
            }

            var item = await this.FindOneAsync(id, cancellationToken: cancellationToken).AnyContext();
            return item is not null;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await this.UpsertAsync(entity, cancellationToken: cancellationToken).AnyContext();
            return result.entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await this.UpsertAsync(entity, cancellationToken: cancellationToken).AnyContext();
            return result.entity;
        }

        public async Task<(TEntity entity, RepositoryActionResult action)> UpsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var isNew = entity.Id.IsDefault() || !await this.ExistsAsync(entity.Id.ToGuid(), cancellationToken: cancellationToken).AnyContext();
            if (isNew)
            {
                entity.Id = Guid.NewGuid().ToString();
                if (entity is IStateEntity stateEntity && stateEntity.State?.CreatedDate == null)
                {
                    stateEntity.State?.SetCreated();
                }
            }

            var partitionKey = this.GetPartitionKey(entity);
            var action = isNew ? RepositoryActionResult.Inserted : RepositoryActionResult.Updated;
            var response = await this.container.UpsertItemAsync(
                entity,
                partitionKey: partitionKey, cancellationToken: cancellationToken).AnyContext();
            return (response.Resource, action);
        }

        public async Task<RepositoryActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id.IsDefault())
            {
                return RepositoryActionResult.None;
            }

            var entity = await this.FindOneAsync(id, cancellationToken: cancellationToken).AnyContext();
            if (entity is not null)
            {
                try
                {
                    var requestOptions = this.GetRequestOptions();
                    var partitionKey = this.GetPartitionKey(entity);
                    var response = await this.container.DeleteItemAsync<TEntity>(
                        entity.Id,
                        partitionKey, cancellationToken: cancellationToken).AnyContext();

                    return response.StatusCode == HttpStatusCode.NoContent ? RepositoryActionResult.None : RepositoryActionResult.Deleted;
                }
                catch (CosmosException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RepositoryActionResult.None;
                    }

                    throw;
                }
            }

            return RepositoryActionResult.None;
        }

        public async Task<RepositoryActionResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity.Id.IsDefault() == true)
            {
                return RepositoryActionResult.None;
            }

            var oldEntity = await this.FindOneAsync(entity.Id.ToGuid(), cancellationToken: cancellationToken).AnyContext();
            if (oldEntity is null)
            {
                return RepositoryActionResult.None;
            }

            try
            {
                var requestOptions = this.GetRequestOptions();
                var partitionKey = this.GetPartitionKey(entity);
                var response = await this.container.DeleteItemAsync<TEntity>(
                    entity.Id,
                    partitionKey, cancellationToken: cancellationToken).AnyContext();

                return response.StatusCode == HttpStatusCode.NoContent ? RepositoryActionResult.None : RepositoryActionResult.Deleted;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return RepositoryActionResult.None;
                }

                throw;
            }
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await this.CountAsync(Enumerable.Empty<ISpecification<TEntity>>(), cancellationToken: cancellationToken).AnyContext();
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.CountAsync(new[] { specification }, cancellationToken: cancellationToken).AnyContext();
        }

        public async Task<int> CountAsync(IEnumerable<ISpecification<TEntity>> specifications, CancellationToken cancellationToken = default)
        {
            var specificationsArray = specifications as ISpecification<TEntity>[] ?? specifications.ToArray();
            var expressions = specificationsArray.Safe().Select(s => s.ToExpression());

            var requestOptions = this.GetRequestOptions();
            var response = await this.container.GetItemLinqQueryable<TEntity>(requestOptions: requestOptions)
                .WhereIf(expressions)
                .CountAsync(cancellationToken: cancellationToken).AnyContext();

            return response;
        }

        public async Task<TEntity?> FindOneAsync(object id, CancellationToken cancellationToken = default)
        {
            if (!id.IsValidGuid())
            {
                return default;
            }

            return await this.FindOneAsync(id.ToString().ToGuid(), cancellationToken).AnyContext();
        }

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            if (!id.IsValidGuid())
            {
                return default;
            }

            return await this.ExistsAsync(id.ToString().ToGuid(), cancellationToken).AnyContext();
        }

        public async Task<RepositoryActionResult> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            if (!id.IsValidGuid())
            {
                return default;
            }

            return await this.DeleteAsync(id.ToString().ToGuid(), cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.FindOneAsync(specification: new Specification<TEntity>(), options, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.FindOneAsync(specification.AsList(), options, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var specificationsArray = specifications != null
                            ? specifications is ISpecification<TEntity>[]? specifications as ISpecification<TEntity>[]
                                    : specifications.ToArray()
                            : null;
            var expressions = specificationsArray.Safe().Select(s => s.ToExpression());
            var order = options?.GetOrders().FirstOrDefault();

            this.Initialize();
            var requestOptions = this.GetRequestOptions();

            double requestCharge = 0;
            var query = this.container.GetItemLinqQueryable<TEntity>(requestOptions: requestOptions)
                .WhereIf(expressions)
                .OrderByIf(order?.Expression, order?.Direction == OrderByDirection.Desc)
                .SkipIf(options?.Skip)
                .TakeIf(1);

            var iterator = this.CosmosDbLinqQuery.GetFeedIterator(query);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken: cancellationToken).AnyContext();
                requestCharge += response.RequestCharge;
                foreach (var result in response.Resource)
                {
                    return result;
                }
            }

            return default;
        }

        private void Initialize()
        {
            if (this.Options.Container == null)
            {
                if (this.Options.ContainerName is not null && !this.IsValidContainerName(this.Options.ContainerName))
                {
                    throw new ArgumentException($"Container name is not valid {this.Options.ContainerName}");
                }

                Database database = this.Options.Client?
                    .CreateDatabaseIfNotExistsAsync(this.Options.Database.IsNullOrEmpty() ? "master" : this.Options.Database, throughput: this.Options.ThroughPut).Result;
                var containerName = this.Options.ContainerName.IsNullOrEmpty() ? typeof(TEntity).PrettyName() : this.Options.Database;

                var containerProperties = new ContainerProperties(containerName, partitionKeyPath: $"/{this.Options.GetPartitionKey()}");
                if (this.Options.IndexingPolicy != null)
                {
                    containerProperties.IndexingPolicy = this.Options.IndexingPolicy;
                }

                if (this.Options.IndexingPolicy != null && this.Options.IndexingPolicy.HasSpatialPath())
                {
                    containerProperties.GeospatialConfig = new GeospatialConfig(GeospatialType.Geometry);
                }

                this.container = database.CreateContainerIfNotExistsAsync(containerProperties, throughput: this.Options.ThroughPut).Result;
            }
            else
            {
                this.container = this.Options.Container;
            }
        }

        private QueryRequestOptions GetRequestOptions()
        {
            var options = new QueryRequestOptions
            {
                MaxItemCount = -1
            };
            return options;
        }

        private PartitionKey GetPartitionKey(TEntity entity)
            => new(this.GetPartitionKeyValue(entity));

        private string GetPartitionKeyValue(TEntity entity)
        {
            if (entity is null)
            {
                return string.Empty;
            }

            return this.Options.PartitionKey.IsNullOrEmpty()
                ? entity.GetPropertyValue(this.Options.PartitionKeyExpression)?.ToString() ?? string.Empty
                : entity.GetPropertyValue(this.Options.PartitionKey) ?? string.Empty;
        }

        private bool IsValidContainerName(string name)
        {
            var invalidNames = new[] { "where", "order", "by", "group", "select", "count", "value", "distinct", "as", "from" };
            return invalidNames.Contains(name.ToLower());
        }
    }
}
