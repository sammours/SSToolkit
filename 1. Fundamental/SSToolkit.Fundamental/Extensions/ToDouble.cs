namespace SSToolkit.Fundamental.Extensions
{
    using System.Globalization;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Convert the string to a null-able double.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static double? ToNullableDouble(this string source, double? @default = null)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = double.TryParse(source, out double parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }

        /// <summary>
        /// Convert the string to an double.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static double ToDouble(this string source, double @default = 0)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = double.TryParse(source, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out double parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }
    }
}
