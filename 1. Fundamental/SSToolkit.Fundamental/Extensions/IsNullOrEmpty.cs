namespace SSToolkit.Fundamental.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if <see cref="IEnumerable<TSource>"/> is null or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is null or empty; otherwise, <c>false</c></returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source) => source == null || !source.Any();

        /// <summary>
        /// Check if <see cref="ICollection<TSource>"/> is null or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is null or empty; otherwise, <c>false</c></returns>
        public static bool IsNullOrEmpty<TSource>(this ICollection<TSource> source) => source == null || !source.Any();

        /// <summary>
        /// Check if <see cref="Stream"/> is null or empty
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is null or empty; otherwise, <c>false</c></returns>
        public static bool IsNullOrEmpty(this Stream source) => source == null || source.Length == 0;

        /// <summary>
        /// Check if <see cref="string"/> is null or empty
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is null or empty; otherwise, <c>false</c></returns>
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);
    }
}
