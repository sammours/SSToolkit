namespace SSToolkit.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;

    /// <summary>
    /// Base interface for all Repository implementations
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity handled by this repository</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> FindAllAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="specification">The <see cref="IEnumerable{ISpecification{T}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/> matching the specification</returns>
        Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="specifications">The <see cref="IEnumerable{ISpecification{T}}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/> matching the specification</returns>
        Task<IEnumerable<TEntity>> FindAllAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives entity from the repository
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>The entity that has the id</returns>
        Task<TEntity?> FindOneAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if entity exists
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>True if exist</returns>
        Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/></returns>
        Task<TEntity?> FindOneAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="specification">The <see cref="IEnumerable{ISpecification{T}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/> matching the specification</returns>
        Task<TEntity?> FindOneAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="specifications">The <see cref="IEnumerable{ISpecification{T}}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/> matching the specification</returns>
        Task<TEntity?> FindOneAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts the provided entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the provided entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert or updates the provided entity.
        /// </summary>
        /// <param name="entity">The entity to insert or update.</param>
        /// <returns></returns>
        Task<(TEntity entity, RepositoryActionResult action)> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete entity matches the id
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns></returns>
        Task<RepositoryActionResult> DeleteAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete entity matches the id
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        Task<RepositoryActionResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="specifications">The specifications.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> CountAsync(IEnumerable<ISpecification<TEntity>> specifications, CancellationToken cancellationToken = default);
    }
}