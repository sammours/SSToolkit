namespace SSToolkit.Fundamental.Extensions
{
    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Convert the string to a null-able int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static int? ToNullableInt(this string source, int? @default = null)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = int.TryParse(source, out int parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }

        /// <summary>
        /// Convert the string to an int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static int ToInt(this string source, int @default = 0)
        {
            if (source.IsNullOrEmpty())
            {
                return @default;
            }

            var result = int.TryParse(source, out int parsedValue);

            if (!result)
            {
                return @default;
            }

            return parsedValue;
        }
    }
}
