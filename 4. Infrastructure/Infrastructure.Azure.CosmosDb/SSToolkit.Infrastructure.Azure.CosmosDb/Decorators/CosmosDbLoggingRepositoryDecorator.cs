namespace SSToolkit.Infrastructure.Azure.CosmosDb.Decorators
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Decorators;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    internal class CosmosDbLoggingRepositoryDecorator<TEntity> : LoggingRepositoryDecorator<TEntity>, ICosmosDbRepository<TEntity>
        where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
    {
        private readonly ICosmosDbRepository<TEntity> decoretee;

        public CosmosDbLoggingRepositoryDecorator(ICosmosDbRepository<TEntity> repository, ILogger<IRepository<TEntity>> logger)
            : base(repository, logger)
        {
            this.decoretee = repository;
        }

        public async Task<RepositoryActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Start deleting {typeof(TEntity)} {id}");
            var result = await this.decoretee.DeleteAsync(id, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {id} is deleted");
            return result;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Start checking if {typeof(TEntity)} {id} exists");
            var result = await this.decoretee.ExistsAsync(id, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {id} exists: {result}");
            return result;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(string query, List<DbParameter> dbParameters, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Get all {typeof(TEntity)} by query: '{query}'.");
            return await this.decoretee.FindAllAsync(query, dbParameters, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Get {typeof(TEntity)} by {id}.");
            return await this.decoretee.FindOneAsync(id, cancellationToken).AnyContext();
        }
    }
}
