namespace SSToolkit.Domain.Repositories
{
    using System;
    using System.Linq.Expressions;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    public class OrderByOption<TEntity>
        where TEntity : IEntity
    {
        public OrderByOption(Expression<Func<TEntity, object>> expression,
           OrderByDirection direction = OrderByDirection.Asc)
        {
            this.Expression = expression;
            this.Direction = direction;
        }

        public Expression<Func<TEntity, object>> Expression { get; }

        public OrderByDirection Direction { get; }

        public override string ToString()
        {
            return $"{this.Expression.ToExpressionString()} - Direction: {this.Direction}";
        }
    }
}
