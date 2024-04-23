namespace SSToolkit.Infrastructure.EntityFrameworkCore.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using SSToolkit.Domain.Repositories.Model;

    public static partial class EntityFrameworkExtensions
    {
        /// <summary>
        /// Skip if the value is provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="skip">Nullable int</param>
        /// <returns></returns>
        public static IQueryable<T> SkipIf<T>(
            this IQueryable<T> source, int? skip)
            where T : class, IEntity
            => skip.HasValue && skip.Value > 0 ? source.Skip(skip.Value) : source;

        /// <summary>
        /// Skip if the value is provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="skip">Nullable int</param>
        /// <returns></returns>
        public static IEnumerable<T> SkipIf<T>(
            this IEnumerable<T> source, int? skip)
            where T : class, IEntity
            => skip.HasValue && skip.Value > 0 ? source.Skip(skip.Value) : source;
    }
}
