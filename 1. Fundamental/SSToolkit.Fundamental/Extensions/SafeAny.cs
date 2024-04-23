namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if the source is null and satisfy the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns><c>true</c> if the source is not null and satisfy the predicate; otherwise, <c>false</c></returns>
        public static bool SafeAny<T>(
            this IEnumerable<T> source,
            Func<T, bool> predicate = null)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }

            if (predicate != null)
            {
                return source.Any(predicate);
            }

            return source.Any();
        }
    }
}
