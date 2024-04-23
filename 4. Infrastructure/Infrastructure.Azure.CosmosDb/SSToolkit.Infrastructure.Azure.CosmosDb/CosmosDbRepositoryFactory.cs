namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Logging;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Decorators;

    public static class CosmosDbRepositoryFactory
    {
        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionString">CosmosDb connection string</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="database">Database name</param>
        /// <param name="container">Container name (default: Entity pretty name)</param>
        /// <param name="throughPut">Through Put (default: 400)</param>
        /// <param name="indexingPolicy">Indexing policy (default: null)</param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(string connectionString, string partitionKey,
            string database, string? container = null, int throughPut = 400, IndexingPolicy? indexingPolicy = null)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(new CosmosDbRepositoryOptions<TEntity>()
            {
                ConnectionString = connectionString,
                Client = new CosmosClient(connectionString),
                Database = database,
                ContainerName = container ?? typeof(TEntity).FullName,
                PartitionKey = partitionKey,
                ThroughPut = throughPut,
                IndexingPolicy = indexingPolicy ?? new IndexingPolicy()
            });

        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connectionString">CosmosDb connection string</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="database">Database name</param>
        /// <param name="container">Container name (default: Entity pretty name)</param>
        /// <param name="throughPut">Through Put (default: 400)</param>
        /// <param name="indexingPolicy">Indexing policy (default: null)</param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(string connectionString, Expression<Func<TEntity, object>> partitionKey,
            string database, string? container = null, int throughPut = 400, IndexingPolicy? indexingPolicy = null)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(new CosmosDbRepositoryOptions<TEntity>()
            {
                ConnectionString = connectionString,
                Client = new CosmosClient(connectionString),
                Database = database,
                ContainerName = container ?? typeof(TEntity).FullName,
                PartitionKeyExpression = partitionKey,
                ThroughPut = throughPut,
                IndexingPolicy = indexingPolicy ?? new IndexingPolicy()
            });

        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="client">CosmosDb client</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="database">Database name</param>
        /// <param name="container">Container name (default: Entity pretty name)</param>
        /// <param name="throughPut">Through Put (default: 400)</param>
        /// <param name="indexingPolicy">Indexing policy (default: null)</param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(CosmosClient client, string partitionKey,
            string database, string? container = null, int throughPut = 400, IndexingPolicy? indexingPolicy = null)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(new CosmosDbRepositoryOptions<TEntity>()
            {
                Client = client,
                Database = database,
                ContainerName = container ?? typeof(TEntity).FullName,
                PartitionKey = partitionKey,
                ThroughPut = throughPut,
                IndexingPolicy = indexingPolicy ?? new IndexingPolicy()
            });

        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="client">CosmosDb client</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="database">Database name</param>
        /// <param name="container">Container name (default: Entity pretty name)</param>
        /// <param name="throughPut">Through Put (default: 400)</param>
        /// <param name="indexingPolicy">Indexing policy (default: null)</param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(CosmosClient client, Expression<Func<TEntity, object>> partitionKey,
            string database, string? container = null, int throughPut = 400, IndexingPolicy? indexingPolicy = null)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(new CosmosDbRepositoryOptions<TEntity>()
            {
                Client = client,
                Database = database,
                ContainerName = container ?? typeof(TEntity).FullName,
                PartitionKeyExpression = partitionKey,
                ThroughPut = throughPut,
                IndexingPolicy = indexingPolicy ?? new IndexingPolicy()
            });

        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="endpointUri">CosmosDb endpoint uri</param>
        /// <param name="authKeyOrResourceToken">CosmosDb auth key</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="database">Database name</param>
        /// <param name="container">Container name (default: Entity pretty name)</param>
        /// <param name="throughPut">Through Put (default: 400)</param>
        /// <param name="indexingPolicy">Indexing policy (default: null)</param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(string endpointUri, string authKeyOrResourceToken,
            string partitionKey, string database, string? container = null, int throughPut = 400, IndexingPolicy? indexingPolicy = null)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(new CosmosDbRepositoryOptions<TEntity>()
            {
                EndPointUri = endpointUri,
                AccountKey = authKeyOrResourceToken,
                Client = new CosmosClient(endpointUri, authKeyOrResourceToken),
                Database = database,
                ContainerName = container ?? typeof(TEntity).FullName,
                PartitionKey = partitionKey,
                ThroughPut = throughPut,
                IndexingPolicy = indexingPolicy ?? new IndexingPolicy()
            });

        /// <summary>
        /// Create ICosmosDbRepository instance for <see cref="Entity{T}"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="options">CosmosDb repository option <see cref="CosmosDbRepositoryOptions{TEntity}"/></param>
        /// <returns>Create CosmosDbRepository repository instance for <see cref="Entity{T}"/></returns>
        public static ICosmosDbRepository<TEntity> Create<TEntity>(CosmosDbRepositoryOptions<TEntity> options)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbRepository<TEntity>(options);

        /// <summary>
        /// Add Logging decorator
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="repository"></param>
        /// <param name="logger">The logger</param>
        /// <returns>Decorated reposiotry</returns>
        public static ICosmosDbRepository<TEntity> AddLoggingDecorator<TEntity>(this ICosmosDbRepository<TEntity> repository, ILogger<ICosmosDbRepository<TEntity>> logger)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
            => new CosmosDbLoggingRepositoryDecorator<TEntity>(repository, logger);
    }
}
