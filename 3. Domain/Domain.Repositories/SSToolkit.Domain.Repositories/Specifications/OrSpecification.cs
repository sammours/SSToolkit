namespace SSToolkit.Domain.Repositories.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SSToolkit.Fundamental.Extensions;

    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> leftSpecification;
        private readonly ISpecification<T> rightSpecification;

        public OrSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
        {
            this.rightSpecification = rightSpecification ?? throw new ArgumentNullException(nameof(leftSpecification));
            this.leftSpecification = leftSpecification ?? throw new ArgumentNullException(nameof(leftSpecification));
        }

        public override Expression<Func<T, bool>> ToExpression()
            => this.leftSpecification.ToExpression().Or(this.rightSpecification.ToExpression());

        public override string ToString() => this.ToExpression().ToString();
    }
}
