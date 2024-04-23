namespace SSToolkit.Fundamental.Extensions
{
    using System;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Substring before the first hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seperator"></param>
        /// <param name="comparison"></param>
        /// <returns>The rest of the string before the first hit </returns>
        public static string SubstringTill(this string source, string seperator, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            return SubstringTillInternal(source, seperator, source.IndexOf(seperator, comparison));
        }

        /// <summary>
        /// Substring before the last hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seperator"></param>
        /// <param name="comparison"></param>
        /// <returns>The rest of the string before the last hit </returns>
        public static string SubstringTillLast(this string source, string seperator, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            return SubstringTillInternal(source, seperator, source.LastIndexOf(seperator, comparison));
        }

        private static string SubstringTillInternal(this string source, string seperator, int index)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            if (index == 0)
            {
                return string.Empty;
            }

            if (index > 0)
            {
                return source.Substring(0, index);
            }

            return source;
        }
    }
}
