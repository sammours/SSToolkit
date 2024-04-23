namespace SSToolkit.Domain.Repositories
{
    using System.Collections.Generic;
    using SSToolkit.Domain.Repositories.Model;

    /// <summary>
    /// Various options to specify the <see cref="IRepository{TEntity}"/> find operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFindOptions<T>
        where T : IEntity
    {
        /// <summary>
        /// Gets or sets the Skip.
        /// </summary>
        int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the Take.
        /// </summary>
        int? Take { get; set; }

        /// <summary>
        /// Gets or sets the Orders.
        /// </summary>
        OrderByOption<T>? Order { get; set; }


        /// <summary>
        /// Gets or sets the Orders.
        /// </summary>
        IEnumerable<OrderByOption<T>>? Orders { get; set; }

        /// <summary>
        /// Gets or sets the Include.
        /// </summary>
        IncludeOption<T>? Include { get; set; }

        /// <summary>
        /// Gets or sets the Includes.
        /// </summary>
        IEnumerable<IncludeOption<T>>? Includes { get; set; }

        /// <summary>
        /// Gets or sets the TrackChanges.
        /// </summary>
        bool TrackChanges { get; set; }

        /// <summary>
        ///  Check if has Orders
        /// </summary>
        /// <returns></returns>
        bool HasOrders();

        /// <summary>
        /// Check if should include
        /// </summary>
        /// <returns></returns>
        bool ShouldInclude();

        /// <summary>
        /// List all Orders
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderByOption<T>> GetOrders();

        /// <summary>
        /// List all Includes
        /// </summary>
        /// <returns></returns>
        IEnumerable<IncludeOption<T>> GetIncludes();
    }
}
