namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Diagnostics;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Slice from a string to a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns>The sliced string</returns>
        [DebuggerStepThrough]
        public static string Slice(this string source, string start, string end, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return SliceFrom(source, start, comparison)
                .SliceTill(end, comparison);
        }

        /// <summary>
        /// Slice from a string to a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns>The sliced string</returns>
        [DebuggerStepThrough]
        public static string Slice(this string source, int start, int end)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            if (end > source.Length)
            {
                return source.Substring(start, source.Length);
            }

            return source.Substring(start, end - start);
        }
    }
}
