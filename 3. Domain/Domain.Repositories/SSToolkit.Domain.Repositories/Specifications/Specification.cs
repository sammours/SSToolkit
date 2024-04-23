namespace SSToolkit.Domain.Repositories.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SSToolkit.Fundamental.Extensions;

    public class Specification<T> : ISpecification<T>
    {
        private static readonly ISpecification<T> All = new Specification<T>();
        private readonly Expression<Func<T, bool>> expression;

        public Specification()
        {
            this.expression = x => true;
        }

        public Specification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        public virtual Expression<Func<T, bool>> ToExpression() => this.expression;

        public Func<T, bool> ToPredicate() => this.ToExpression().Compile();

        public bool IsSatisfiedBy(T entity)
        {
            if (entity == null)
            {
                return false;
            }

            Func<T, bool> predicate = this.ToPredicate();
            return predicate(entity);
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            if (this == All)
            {
                return specification;
            }

            if (specification == All)
            {
                return this;
            }

            return new AndSpecification<T>(this, specification);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            if (this == All || specification == All)
            {
                return All;
            }

            return new OrSpecification<T>(this, specification);
        }

        public ISpecification<T> Not() => new NotSpecification<T>(this);

        public override string ToString()
            => this.expression.ToExpressionString();
    }
}
