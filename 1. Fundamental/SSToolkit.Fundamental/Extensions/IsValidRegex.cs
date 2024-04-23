namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Validate a string against a pattern
        /// </summary>
        /// <param name="source">the source string date</param>
        /// <param name="pattern">The regex pattern</param>
        /// <returns><c>true</c> if is valid; otherwise, <c>false</c></returns>
        public static bool IsValidRegex(this string source, string pattern)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }

            if (!pattern.IsValidRegexPattern())
            {
                return false;
            }

            return Regex.Match(source.Trim(), pattern.Trim()).Success;
        }

        /// <summary>
        /// Validate a regex pattern
        /// </summary>
        /// <param name="pattern">The regex pattern</param>
        /// <returns><c>true</c> if is valid; otherwise, <c>false</c></returns>
        public static bool IsValidRegexPattern(this string pattern)
        {
            if (pattern.IsNullOrEmpty())
            {
                return false;
            }

            try
            {
                Regex.Match(string.Empty, pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
