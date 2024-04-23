namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Fundamental.Extensions;
    using SSToolkit.Infrastructure.EntityFrameworkCore.Extensions;

    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;

        public EntityFrameworkRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            if (options?.HasOrders() == true)
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take)
                    .OrderByIf(options).ToListAsyncSafe(cancellationToken).AnyContext();
            }
            else
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take).ToListAsyncSafe(cancellationToken).AnyContext();
            }
        }

        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="specification">The <see cref="IEnumerable{ISpecification{T}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/> matching the specification</returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            if (options?.HasOrders() == true)
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .WhereIf(specification.ToExpression())
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take)
                    .OrderByIf(options).ToListAsyncSafe(cancellationToken).AnyContext();
            }
            else
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .WhereIf(specification.ToExpression())
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take).ToListAsyncSafe(cancellationToken).AnyContext();
            }
        }

        /// <summary>
        /// Retrives entities from the repository
        /// </summary>
        /// <param name="specifications">The <see cref="IEnumerable{ISpecification{T}}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="IEnumerable{T}"/> matching the specification</returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var expressions = specifications.Safe().Select(s => s.ToExpression());

            if (options?.HasOrders() == true)
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .WhereIf(expressions)
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take)
                    .OrderByIf(options).ToListAsyncSafe(cancellationToken).AnyContext();
            }
            else
            {
                return await this.dbContext.Set<TEntity>()
                    .TrackChangesIf(options)
                    .IncludeIf(options)
                    .WhereIf(expressions)
                    .SkipIf(options?.Skip)
                    .TakeIf(options?.Take).ToListAsyncSafe(cancellationToken).AnyContext();
            }
        }

        /// <summary>
        /// Retrives entity from the repository
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>The entity that has the id</returns>
        public async Task<TEntity?> FindOneAsync(object id, CancellationToken cancellationToken = default)
        {
            if (id.IsDefault())
            {
                return default;
            }

            return await this.dbContext.Set<TEntity>().FindAsync(new[] { this.CastObjectCheck(id) }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if entity exists
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>True if exist</returns>
        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            if (id.IsDefault())
            {
                return false;
            }

            return await this.FindOneAsync(id, cancellationToken).AnyContext() != null;
        }

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/></returns>
        public async Task<TEntity?> FindOneAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Set<TEntity>()
                .TrackChangesIf(options)
                .IncludeIf(options)
                .SkipIf(options?.Skip)
                .TakeIf(options?.Take)
                .OrderByIf(options).FirstOrDefaultAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="specification">The <see cref="IEnumerable{ISpecification{T}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/> matching the specification</returns>
        public async Task<TEntity?> FindOneAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Set<TEntity>()
                .TrackChangesIf(options)
                .IncludeIf(options)
                .WhereIf(specification.ToExpression())
                .SkipIf(options?.Skip)
                .TakeIf(options?.Take)
                .OrderByIf(options).FirstOrDefaultAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Retrives first entity from the repository
        /// </summary>
        /// <param name="specifications">The <see cref="IEnumerable{ISpecification{T}}"/> to filter entites</param>
        /// <param name="options">Query specific options</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns><see cref="object{T}"/> matching the specification</returns>
        public async Task<TEntity?> FindOneAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
        {
            var expressions = specifications.Safe().Select(s => s.ToExpression());

            return await this.dbContext.Set<TEntity>()
                .TrackChangesIf(options)
                .IncludeIf(options)
                .WhereIf(expressions)
                .SkipIf(options?.Skip)
                .TakeIf(options?.Take)
                .OrderByIf(options).FirstOrDefaultAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Inserts the provided entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await this.UpsertAsync(entity, cancellationToken).AnyContext();
            return result.entity;
        }

        /// <summary>
        /// Updates the provided entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await this.UpsertAsync(entity, cancellationToken).AnyContext();
            return result.entity;
        }

        /// <summary>
        /// Insert or updates the provided entity.
        /// </summary>
        /// <param name="entity">The entity to insert or update.</param>
        /// <returns></returns>
        public async Task<(TEntity entity, RepositoryActionResult action)> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            bool isNew = entity.Id.IsDefault() || !await this.ExistsAsync(entity.Id, cancellationToken).AnyContext();
            if (isNew)
            {
                this.dbContext.Set<TEntity>().Add(entity);
            }

            await this.dbContext.SaveChangesAsync<TEntity>(cancellationToken).AnyContext();

#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
            return isNew ? (entity, RepositoryActionResult.Inserted) : (entity, RepositoryActionResult.Updated);
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
        }

        /// <summary>
        /// Delete entity matches the id
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns></returns>
        public async Task<RepositoryActionResult> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            if (id.IsDefault())
            {
                return RepositoryActionResult.None;
            }

            var entity = await this.FindOneAsync(id, cancellationToken).AnyContext();
            if (entity != null)
            {
                this.dbContext.Remove(entity);
                await this.dbContext.SaveChangesAsync(cancellationToken).AnyContext();
                return RepositoryActionResult.Deleted;
            }

            return RepositoryActionResult.None;
        }

        /// <summary>
        /// Delete entity matches the id
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        public async Task<RepositoryActionResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null || entity.Id.IsDefault())
            {
                return RepositoryActionResult.None;
            }

            return await this.DeleteAsync(entity.Id, cancellationToken).AnyContext();
        }

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Set<TEntity>().CountAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Set<TEntity>()
                        .WhereIf(specification.ToExpression())
                        .CountAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Counts all entities.
        /// </summary>
        /// <param name="specifications">The specifications.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task<int> CountAsync(IEnumerable<ISpecification<TEntity>> specifications, CancellationToken cancellationToken = default)
        {
            var expressions = specifications.Safe().Select(s => s.ToExpression());
            return await this.dbContext.Set<TEntity>()
                       .WhereIf(expressions)
                       .CountAsync(cancellationToken).AnyContext();
        }

        private object CastObjectCheck(object value)
        {
            try
            {
                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(Guid) && value?.GetType() == typeof(string))
                {
                    return value?.ToString() ?? string.Empty;
                }

                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(Guid) && value?.GetType() == typeof(Guid))
                {
                    return Guid.Parse(value?.ToString() ?? string.Empty);
                }

                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(int) && value?.GetType() == typeof(int))
                {
                    return int.Parse(value?.ToString() ?? string.Empty);
                }

                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(float) && value?.GetType() == typeof(float))
                {
                    return float.Parse(value?.ToString() ?? string.Empty);
                }

                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(double) && value?.GetType() == typeof(double))
                {
                    return double.Parse(value?.ToString() ?? string.Empty);
                }

                if (typeof(TEntity).GetProperty("Id")?.PropertyType == typeof(string) && value?.GetType() == typeof(string))
                {
                    return value?.ToString() ?? string.Empty;
                }

                return value ?? string.Empty;
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message, ex);
            }
        }
    }
}
