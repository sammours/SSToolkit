namespace SSToolkit.Infrastructure.Azure.CosmosDb.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// IQueryable extensions.
    /// </summary>
    public static partial class CosmosExtensionHelpers
    {
        /// <summary>
        /// Where when where is provided
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> expression)
        {
            if (expression != null)
            {
                return source.Where(expression);
            }

            return source;
        }

        /// <summary>
        /// Where when where is provided
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(
            this IQueryable<TEntity> source,
            IEnumerable<Expression<Func<TEntity, bool>>> expressions)
        {
            if (expressions?.Any() == true)
            {
                foreach (var expression in expressions)
                {
                    source = source.Where(expression);
                }
            }

            return source;
        }
    }
}
