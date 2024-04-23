namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    using Microsoft.Extensions.Logging;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Decorators;
    using SSToolkit.Domain.Repositories.Model;

    public static class EntityFrameworkRepositoryFactory
    {
        /// <summary>
        /// Create IRepository instance
        /// </summary>
        /// <typeparam name="TEntity">Domain Entity</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <returns>Create Entityframework repository instance</returns>
        public static IRepository<TEntity> Create<TEntity>(BaseDbContext dbContext)
            where TEntity : class, IEntity
            => new EntityFrameworkRepository<TEntity>(dbContext);

        /// <summary>
        /// Add Logging decorator
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="repository"></param>
        /// <param name="logger">The logger</param>
        /// <returns>Decorated reposiotry</returns>
        public static IRepository<TEntity> AddLoggingDecorator<TEntity>(this IRepository<TEntity> repository, ILogger<IRepository<TEntity>> logger)
            where TEntity : class, IEntity
            => new LoggingRepositoryDecorator<TEntity>(repository, logger);
    }
}
