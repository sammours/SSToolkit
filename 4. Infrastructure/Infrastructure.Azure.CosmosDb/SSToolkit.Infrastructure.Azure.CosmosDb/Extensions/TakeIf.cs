namespace SSToolkit.Infrastructure.Azure.CosmosDb.Extensions
{
    using System.Linq;

    public static partial class CosmosExtensionHelpers
    {
        /// <summary>
        /// Take when take is provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IQueryable<T> TakeIf<T>(
            this IQueryable<T> source,
            int? count = null)
        {
            if (count.HasValue && count.Value > 0)
            {
                return source.Take(count.Value);
            }

            return source;
        }
    }
}
