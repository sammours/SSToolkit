namespace SSToolkit.Infrastructure.EntityFrameworkCore.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static partial class EntityFrameworkExtensions
    {
        /// <summary>
        /// Take if the value > 0
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="maxItemCount">The take</param>
        /// <returns></returns>
        public static IQueryable<TSource> TakeIf<TSource>(
            this IQueryable<TSource> source, int maxItemCount)
            => maxItemCount > 0 ? source.Take(maxItemCount) : source;

        /// <summary>
        /// Take if the value > 0
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="maxItemCount">The take</param>
        /// <returns></returns>
        public static IEnumerable<TSource> TakeIf<TSource>(
            this IEnumerable<TSource> source, int maxItemCount)
            => maxItemCount > 0 ? source.Take(maxItemCount) : source;

        /// <summary>
        /// Take if the value is not null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="maxItemCount">Nullable take</param>
        /// <returns></returns>
        public static IQueryable<TSource> TakeIf<TSource>(
            this IQueryable<TSource> source, int? maxItemCount)
            => maxItemCount.HasValue && maxItemCount.Value > 0 ? source.Take((int)maxItemCount) : source;

        /// <summary>
        /// Take if the value is not null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="maxItemCount">Nullable take</param>
        /// <returns></returns>
        public static IEnumerable<TSource> TakeIf<TSource>(
            this IEnumerable<TSource> source, int? maxItemCount)
            => maxItemCount.HasValue && maxItemCount.Value > 0 ? source.Take((int)maxItemCount) : source;
    }
}
