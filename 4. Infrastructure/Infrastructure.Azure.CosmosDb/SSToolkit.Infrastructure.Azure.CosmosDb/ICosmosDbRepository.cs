namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Model;

    public interface ICosmosDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
    {
        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="dbParameters">List of db parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/> matching the specification</returns>
        Task<IEnumerable<TEntity>> FindAllAsync(string query, List<DbParameter> dbParameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives entity from the repository
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>The entity that has the id</returns>
        Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Check if entity exists
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>True if exist</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete entity matches the id
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns></returns>
        Task<RepositoryActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
