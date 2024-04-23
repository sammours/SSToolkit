namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Safe check if source contains a value.
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="value">The value to search for</param>
        /// <param name="comp">String Comparison</param>
        /// <returns><c>true</c>, if both the source and the value are not null and the source contains the value; otherwise, <c>false</c></returns>
        public static bool Contains(this string source, string value, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return source.IndexOf(value, comp) >= 0;
        }

        /// <summary>
        /// Safe check if source contains a value.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable<string>"/></param>
        /// <param name="value">The value to search for</param>
        /// <param name="comp">String Comparison</param>
        /// <returns><c>true</c>, if both the source and the value are not null and the source contains the value; otherwise, <c>false</c></returns>
        public static bool Contains(this IEnumerable<string> source, string value, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }

            return !value.IsNullOrEmpty() && source.Any(x => string.Compare(x, value, comp) == 0);
        }

        /// <summary>
        /// Determines whether the specified string contains any of the items.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="items">The items.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///  <c>true</c> if the specified items contains any; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsAny(
            this string source,
            string[] items,
            StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (items == null)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                if (source.Contains(item, comp))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
