namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Diagnostics;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Slice string after the first hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="from">The start string</param>
        /// <param name="comparison"></param>
        /// <returns>Sliced string</returns>
        [DebuggerStepThrough]
        public static string SliceFrom(this string source, string from, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (from.IsNullOrEmpty())
            {
                return source;
            }

            return SliceFromInternal(source, from, source.IndexOf(from, comparison));
        }

        /// <summary>
        /// Slice string after the last hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="from">The start string</param>
        /// <param name="comparison"></param>
        /// <returns>Sliced string</returns>
        [DebuggerStepThrough]
        public static string SliceFromLast(this string source, string from, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (from.IsNullOrEmpty())
            {
                return source;
            }

            return SliceFromInternal(source, from, source.LastIndexOf(from, comparison));
        }

        private static string SliceFromInternal(this string source, string from, int fromIndex)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (from.IsNullOrEmpty())
            {
                return source;
            }

            if (fromIndex == 0 && fromIndex + from.Length < source.Length)
            {
                return source.Substring(fromIndex + from.Length);
            }

            if (fromIndex > 0 && fromIndex == source.Length)
            {
                return string.Empty;
            }

            if (fromIndex > 0 && fromIndex + from.Length < source.Length)
            {
                return source.Substring(fromIndex + from.Length);
            }

            return string.Empty;
        }
    }
}
