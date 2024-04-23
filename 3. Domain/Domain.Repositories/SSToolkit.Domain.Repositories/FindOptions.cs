namespace SSToolkit.Domain.Repositories
{
    using System.Collections.Generic;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    /// <summary>
    /// Various options to specify the <see cref="IRepository{T}" /> find operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IFindOptions{T}" />
    public class FindOptions<T> : IFindOptions<T>
        where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindOptions{T}"/> class.
        /// </summary>
        public FindOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindOptions{T}"/> class.
        /// </summary>
        /// <param name="skip">The skip amount.</param>
        /// <param name="take">The take amount.</param>
        /// <param name="order">The order option</param>
        /// <param name="orders">The order options</param>
        public FindOptions(int? skip = null, int? take = null, OrderByOption<T>? order = null, IEnumerable<OrderByOption<T>>? orders = null)
        {
            this.Take = take;
            this.Skip = skip;
            this.Order = order;
            this.Orders = orders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindOptions{T}"/> class.
        /// </summary>
        /// <param name="skip">The skip amount.</param>
        /// <param name="take">The take amount.</param>
        /// <param name="include">The include option</param>
        /// <param name="includes">The include options</param>
        public FindOptions(int? skip = null, int? take = null, IncludeOption<T>? include = null, IEnumerable<IncludeOption<T>>? includes = null)
        {
            this.Take = take;
            this.Skip = skip;
            this.Include = include;
            this.Includes = includes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindOptions{T}"/> class.
        /// </summary>
        /// <param name="skip">The skip amount.</param>
        /// <param name="take">The take amount.</param>
        /// <param name="order">The order option</param>
        /// <param name="orders">The order options</param>
        /// <param name="include">The include option</param>
        /// <param name="includes">The include options</param>
        public FindOptions(int? skip = null, int? take = null, OrderByOption<T>? order = null, IEnumerable<OrderByOption<T>>? orders = null, IncludeOption<T>? include = null, IEnumerable<IncludeOption<T>>? includes = null)
        {
            this.Take = take;
            this.Skip = skip;
            this.Order = order;
            this.Orders = orders;
            this.Include = include;
            this.Includes = includes;
        }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public OrderByOption<T>? Order { get; set; }

        public IEnumerable<OrderByOption<T>>? Orders { get; set; }

        public IncludeOption<T>? Include { get; set; }

        public IEnumerable<IncludeOption<T>>? Includes { get; set; }

        public bool TrackChanges { get; set; }

        public IEnumerable<IncludeOption<T>> GetIncludes()
        {
            if (this.Include != null)
            {
                return new List<IncludeOption<T>>
                {
                    this.Include
                }.SafeConcat(this.Includes);
            }

            return this.Includes is not null ? this.Includes : new List<IncludeOption<T>>();
        }

        public IEnumerable<OrderByOption<T>> GetOrders()
        {
            if (this.Order != null)
            {
                return new List<OrderByOption<T>>
                {
                    this.Order
                }.SafeConcat(this.Orders);
            }

            return this.Orders is not null ? this.Orders : new List<OrderByOption<T>>();
        }

        public bool HasOrders() => this.Order != null || this.Orders.SafeAny();

        public bool ShouldInclude() => this.Include != null || this.Includes.SafeAny();
    }
}
