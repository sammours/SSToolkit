namespace SSToolkit.Fundamental.Extensions
{
    using System;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Substring after the first hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seperator"></param>
        /// <param name="comparison"></param>
        /// <returns>The rest of the string after the first hit </returns>
        public static string SubstringFrom(this string source, string seperator, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            return SubstringFromInternal(source, seperator, source.IndexOf(seperator, comparison));
        }

        /// <summary>
        /// Substring after the last hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seperator"></param>
        /// <param name="comparison"></param>
        /// <returns>The rest of the string after the last hit </returns>
        public static string SubstringFromLast(this string source, string seperator, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            return SubstringFromInternal(source, seperator, source.LastIndexOf(seperator, comparison));
        }

        private static string SubstringFromInternal(this string source, string seperator, int index)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (seperator.IsNullOrEmpty())
            {
                return source;
            }

            if (index == 0 && index + seperator.Length < source.Length)
            {
                return source.Substring(index + seperator.Length);
            }

            if (index > 0 && index == source.Length)
            {
                return string.Empty;
            }

            if (index > 0 && index + seperator.Length < source.Length)
            {
                return source.Substring(index + seperator.Length);
            }

            return string.Empty;
        }
    }
}
