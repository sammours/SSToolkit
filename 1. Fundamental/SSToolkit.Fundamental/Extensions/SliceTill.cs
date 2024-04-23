namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Diagnostics;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Slice string before the first hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="from">The start string</param>
        /// <param name="comparison"></param>
        /// <returns>Sliced string</returns>
        [DebuggerStepThrough]
        public static string SliceTill(this string source, string till, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (till.IsNullOrEmpty())
            {
                return source;
            }

            return SliceTillInternal(source, source.IndexOf(till, comparison));
        }

        /// <summary>
        /// Slice string before the last hit of a certain string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="from">The start string</param>
        /// <param name="comparison"></param>
        /// <returns>Sliced string</returns>
        [DebuggerStepThrough]
        public static string SliceTillLast(this string source, string till, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (till.IsNullOrEmpty())
            {
                return source;
            }

            return SliceTillInternal(source, source.LastIndexOf(till, comparison));
        }

        private static string SliceTillInternal(this string source, int tillIndex)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (tillIndex == 0)
            {
                return string.Empty;
            }

            if (tillIndex > 0)
            {
                return source.Substring(0, tillIndex);
            }

            return source;
        }
    }
}
