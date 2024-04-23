namespace SSToolkit.Fundamental.Extensions
{
    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Convert the string to a null-able decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static decimal? ToNullableDecimal(this string source, decimal? @default = null)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = decimal.TryParse(source, out decimal parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }

        /// <summary>
        /// Convert the string to a decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">if set to <c>true</c> [default].</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source, decimal @default = 0)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = decimal.TryParse(source, out decimal parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }
    }
}
