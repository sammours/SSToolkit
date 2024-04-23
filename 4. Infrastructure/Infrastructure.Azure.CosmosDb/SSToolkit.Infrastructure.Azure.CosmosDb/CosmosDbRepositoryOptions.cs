namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Azure.Cosmos;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    public class CosmosDbRepositoryOptions<TEntity>
        where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
    {
        /// <summary>
        /// Get and set CosmosClient
        /// </summary>
        public CosmosClient? Client { get; set; }

        /// <summary>
        /// Get and set Container
        /// </summary>
        public Container? Container { get; set; }

        /// <summary>
        /// Get and set connection string
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Get and set connection string
        /// </summary>
        public string? EndPointUri { get; set; }

        /// <summary>
        /// Gets and sets account key
        /// </summary>
        public string? AccountKey { get; set; }

        /// <summary>
        /// Gets and sets database name (default: master)
        /// </summary>
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// Gets and sets container (default: Entity pretty name)
        /// </summary>
        public string? ContainerName { get; set; }

        /// <summary>
        /// Gets and sets partition key
        /// </summary>
        public string? PartitionKey { get; set; }

        /// <summary>
        /// Gets and sets partition key
        /// </summary>
        public Expression<Func<TEntity, object>>? PartitionKeyExpression { get; set; }

        /// <summary>
        /// Gets and sets indexing policy
        /// </summary>
        public IndexingPolicy? IndexingPolicy { get; set; }

        /// <summary>
        /// Gets and sets through put (default: 400)
        /// </summary>
        public int? ThroughPut { get; set; } = 400;

        public string GetPartitionKey()
        {
            if (!this.PartitionKey.IsNullOrEmpty())
            {
                return this.PartitionKey ?? string.Empty;
            }

            return this.GetPropertyName(this.PartitionKeyExpression?.Body);
        }

        private string GetPropertyName(Expression? exp)
        {
            if (exp is null)
            {
                throw new ArgumentNullException("Property cannot be null");
            }

            switch (exp.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)exp).Member.Name;
                case ExpressionType.Convert:
                    return this.GetPropertyName(((UnaryExpression)exp).Operand);
                default:
                    throw new NotSupportedException(exp.NodeType.ToString());
            }
        }
    }
}
