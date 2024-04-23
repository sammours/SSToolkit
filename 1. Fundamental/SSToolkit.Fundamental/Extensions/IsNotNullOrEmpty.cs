namespace SSToolkit.Fundamental.Extensions
{
    using System.Collections.Generic;
    using System.IO;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if <see cref="IEnumerable<TSource>"/> is not null or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is not null or empty; otherwise, <c>false</c></returns>
        public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source) => !source.IsNullOrEmpty();

        /// <summary>
        /// Check if <see cref="ICollection<TSource>"/> is not null or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is not null or empty; otherwise, <c>false</c></returns>
        public static bool IsNotNullOrEmpty<TSource>(this ICollection<TSource> source) => !source.IsNullOrEmpty();

        /// <summary>
        /// Check if <see cref="Stream"/> is not null or empty
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is not null or empty; otherwise, <c>false</c></returns>
        public static bool IsNotNullOrEmpty(this Stream source) => !source.IsNullOrEmpty();

        /// <summary>
        /// Check if <see cref="string"/> is not null or empty
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns><c>true</c> when source is not null or empty; otherwise, <c>false</c></returns>
        public static bool IsNotNullOrEmpty(this string source) => !source.IsNullOrEmpty();
    }
}
