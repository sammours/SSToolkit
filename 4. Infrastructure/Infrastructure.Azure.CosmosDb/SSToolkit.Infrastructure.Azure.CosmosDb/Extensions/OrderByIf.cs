namespace SSToolkit.Infrastructure.Azure.CosmosDb.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static partial class CosmosExtensionHelpers
    {
        /// <summary>
        /// Order by when order is provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByIf<T, TKey>(
            this IQueryable<T> source,
            Expression<Func<T, TKey>>? expression, bool descending = false)
        {
            if (expression != null)
            {
                if (descending)
                {
                    return source.OrderByDescending(expression);
                }

                return source.OrderBy(expression);
            }

            return source;
        }
    }
}
