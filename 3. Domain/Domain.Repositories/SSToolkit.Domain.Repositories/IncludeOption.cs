namespace SSToolkit.Domain.Repositories
{
    using System;
    using System.Linq.Expressions;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    public class IncludeOption<TEntity>
        where TEntity : IEntity
    {
        public IncludeOption(Expression<Func<TEntity, object>> expression)
        {
            this.Expression = expression;
        }

        public IncludeOption(string path)
        {
            this.Path = path;
        }

        public Expression<Func<TEntity, object>>? Expression { get; }

        public string? Path { get; }

        public override string ToString()
        {
            return this.Expression.ToExpressionString();
        }
    }
}
