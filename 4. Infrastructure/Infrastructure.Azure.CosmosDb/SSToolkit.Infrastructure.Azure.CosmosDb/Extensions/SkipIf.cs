namespace SSToolkit.Infrastructure.Azure.CosmosDb.Extensions
{
    using System.Linq;

    public static partial class CosmosExtensionHelpers
    {
        /// <summary>
        /// Skip when skip is provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IQueryable<T> SkipIf<T>(
            this IQueryable<T> source,
            int? count = null)
        {
            if (count.HasValue && count.Value > 0)
            {
                return source.Skip(count.Value);
            }

            return source;
        }
    }
}
