namespace SSToolkit.Domain.Repositories.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SSToolkit.Fundamental.Extensions;

    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public override Expression<Func<T, bool>> ToExpression()
            => this.specification.ToExpression().Not();
    }
}
