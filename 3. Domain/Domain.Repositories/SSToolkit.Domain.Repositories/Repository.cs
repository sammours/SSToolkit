namespace SSToolkit.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Fundamental.Extensions;

    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly IRepository<TEntity> decoratee;

        protected Repository(IRepository<TEntity> decoratee)
        {
            this.decoratee = decoratee;
        }

        public virtual async Task<RepositoryActionResult> DeleteAsync(object id, CancellationToken cancellationToken = default)
            => await this.decoratee.DeleteAsync(id, cancellationToken).AnyContext();

        public virtual async Task<RepositoryActionResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await this.decoratee.DeleteAsync(entity, cancellationToken).AnyContext();

        public virtual async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
            => await this.decoratee.ExistsAsync(id, cancellationToken).AnyContext();

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindAllAsync(options, cancellationToken).AnyContext();

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindAllAsync(specification, options, cancellationToken).AnyContext();

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindAllAsync(specifications, options, cancellationToken).AnyContext();

        public virtual async Task<TEntity?> FindOneAsync(object id, CancellationToken cancellationToken = default)
            => await this.decoratee.FindOneAsync(id, cancellationToken).AnyContext();

        public virtual async Task<TEntity?> FindOneAsync(IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindOneAsync(options, cancellationToken).AnyContext();

        public virtual async Task<TEntity?> FindOneAsync(ISpecification<TEntity> specification, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindOneAsync(specification, options, cancellationToken).AnyContext();

        public virtual async Task<TEntity?> FindOneAsync(IEnumerable<ISpecification<TEntity>> specifications, IFindOptions<TEntity>? options = null, CancellationToken cancellationToken = default)
            => await this.decoratee.FindOneAsync(specifications, options, cancellationToken).AnyContext();

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await this.decoratee.InsertAsync(entity, cancellationToken).AnyContext();

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await this.decoratee.UpdateAsync(entity, cancellationToken).AnyContext();

        public virtual async Task<(TEntity entity, RepositoryActionResult action)> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await this.decoratee.UpsertAsync(entity, cancellationToken).AnyContext();

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await this.decoratee.CountAsync(cancellationToken).AnyContext();
        }

        public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await this.decoratee.CountAsync(specification, cancellationToken).AnyContext();
        }

        public async Task<int> CountAsync(IEnumerable<ISpecification<TEntity>> specifications, CancellationToken cancellationToken = default)
        {
            return await this.decoratee.CountAsync(specifications, cancellationToken).AnyContext();
        }
    }
}
