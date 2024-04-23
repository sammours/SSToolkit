namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System.Linq;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using SSToolkit.Domain.Repositories.Model;

#pragma warning disable SA1649 // File name should match first type name
    public interface ICosmosDbLinqQuery
#pragma warning restore SA1649 // File name should match first type name
    {
        FeedIterator<TEntity> GetFeedIterator<TEntity>(IQueryable<TEntity> source)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity;
    }

    public class CosmosDbLinqQuery : ICosmosDbLinqQuery
    {
        public FeedIterator<TEntity> GetFeedIterator<TEntity>(IQueryable<TEntity> source)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            return source.ToFeedIterator();
        }
    }
}
