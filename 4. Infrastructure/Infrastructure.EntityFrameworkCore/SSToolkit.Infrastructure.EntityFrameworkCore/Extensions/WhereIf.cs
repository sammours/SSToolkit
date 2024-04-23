namespace SSToolkit.Infrastructure.EntityFrameworkCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static partial class EntityFrameworkExtensions
    {
        /// <summary>
        /// Where expression when expression is provided
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> expression)
        {
            if (expression != null)
            {
                return source.Where(expression);
            }

            return source;
        }

        /// <summary>
        /// Where expression when expression is provided
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(
           this IEnumerable<TSource> source,
           Expression<Func<TSource, bool>> expression)
        {
            if (expression != null)
            {
                return source.Where(expression.Compile()).AsEnumerable();
            }

            return source;
        }

        /// <summary>
        /// Where expression when expression is provided
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            IEnumerable<Expression<Func<TSource, bool>>> expressions)
        {
            var expressionsArray = expressions as Expression<Func<TSource, bool>>[] ?? expressions?.ToArray();
            if (expressionsArray?.Length > 0)
            {
                foreach (var expression in expressionsArray)
                {
                    source = source.Where(expression);
                }
            }

            return source;
        }
    }
}
