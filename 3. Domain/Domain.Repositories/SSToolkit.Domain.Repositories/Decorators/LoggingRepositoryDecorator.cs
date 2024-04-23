namespace SSToolkit.Domain.Repositories.Decorators
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Fundamental.Extensions;

    public class LoggingRepositoryDecorator<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly IRepository<TEntity> decoretee;

        public LoggingRepositoryDecorator(IRepository<TEntity> repository, ILogger<IRepository<TEntity>> logger)
        {
            this.decoretee = repository;
            this.Logger = logger;
        }

        protected ILogger<IRepository<TEntity>> Logger { get; }

        public async Task<RepositoryActionResult> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start deleting {typeof(TEntity)} {id}");
            var result = await this.decoretee.DeleteAsync(id, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {id} is deleted");
            return result;
        }

        public async Task<RepositoryActionResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start deleting {typeof(TEntity)} {entity.Id}");
            var result = await this.decoretee.DeleteAsync(entity, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {entity.Id} is deleted");
            return result;
        }

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start checking if {typeof(TEntity)} {id} exists");
            var result = await this.decoretee.ExistsAsync(id, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {id} exists: {result}");
            return result;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get all {typeof(TEntity)}.", null, options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindAllAsync(options, cancellationToken).AnyContext();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get all {typeof(TEntity)}.", specification.AsList(), options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindAllAsync(specification, options, cancellationToken).AnyContext();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get all {typeof(TEntity)}.", specifications, options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindAllAsync(specifications, options, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(object id, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Get {typeof(TEntity)} by {id}.");
            return await this.decoretee.FindOneAsync(id, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get first {typeof(TEntity)}.", null, options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindOneAsync(options, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get first {typeof(TEntity)}.", specification.AsList(), options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindOneAsync(specification, options, cancellationToken).AnyContext();
        }

        public async Task<TEntity?> FindOneAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            this.LogSpecificationTakeSkipWhenNotNull($"Get first {typeof(TEntity)}.", specifications, options);
            this.LogOrders(options);
            this.LogIncludes(options);

            return await this.decoretee.FindOneAsync(specifications, options, cancellationToken).AnyContext();
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start inserting {typeof(TEntity)}");
            var result = await this.decoretee.InsertAsync(entity, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {result.Id} is inserted");
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start updating {typeof(TEntity)}");
            var result = await this.decoretee.UpdateAsync(entity, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {result.Id} is updated");
            return result;
        }

        public async Task<(TEntity entity, RepositoryActionResult action)> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Start upserting {typeof(TEntity)}");
            var result = await this.decoretee.UpsertAsync(entity, cancellationToken).AnyContext();
            this.Logger.LogInformation($"{typeof(TEntity)} {result.entity.Id} is {result.action}");
            return result;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Get counts fors {typeof(TEntity)}.");
            var result = await this.decoretee.CountAsync(cancellationToken).AnyContext();
            return result;
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            this.Logger.LogInformation($"Get counts fors {typeof(TEntity)}. Specification: {specification}.");
            var result = await this.decoretee.CountAsync(specification, cancellationToken).AnyContext();
            return result;
        }

        public async Task<int> CountAsync(IEnumerable<ISpecification<TEntity>> specifications, CancellationToken cancellationToken = default)
        {
            var loggingMessage = new StringBuilder($"Get counts fors {typeof(TEntity)}.");
            foreach (var specification in specifications.Safe())
            {
                loggingMessage.Append($" Specification: {specification}.");
            }

            this.Logger.LogInformation(loggingMessage.ToString().Trim());
            var result = await this.decoretee.CountAsync(specifications, cancellationToken).AnyContext();
            return result;
        }

        private void LogSpecificationTakeSkipWhenNotNull(string message, IEnumerable<ISpecification<TEntity>>? specifications, IFindOptions<TEntity>? options)
        {
            var loggingMessage = new StringBuilder(message);
            if (specifications != null)
            {
                foreach (var specification in specifications.Safe())
                {
                    loggingMessage.Append($" Specification: {specification}.");
                }
            }

            if (options != null)
            {
                if (options.Take.HasValue)
                {
                    loggingMessage.Append($" Take: {options.Take.Value}.");
                }

                if (options.Skip.HasValue)
                {
                    loggingMessage.Append($" Skip: {options.Skip.Value}.");
                }
            }

            this.Logger.LogInformation(loggingMessage.ToString().Trim());
        }

        private void LogOrders(IFindOptions<TEntity>? options)
        {
            if (options != null && options.HasOrders())
            {
                var loggingMessage = new StringBuilder();
                foreach (var order in options.GetOrders())
                {
                    loggingMessage.Append($"Order: {order}. ");
                }

                this.Logger.LogInformation(loggingMessage.ToString().Trim());
            }
        }

        private void LogIncludes(IFindOptions<TEntity>? options)
        {
            if (options != null && options.ShouldInclude())
            {
                var loggingMessage = new StringBuilder();
                foreach (var include in options.GetIncludes())
                {
                    loggingMessage.Append($"Include: {include}. ");
                }

                this.Logger.LogInformation(loggingMessage.ToString().Trim());
            }
        }
    }
}
