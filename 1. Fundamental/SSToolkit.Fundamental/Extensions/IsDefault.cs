namespace SSToolkit.Fundamental.Extensions
{
    using System;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if <see cref="object"/> has a default value
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> when the source has a default value; otherwise, <c>false</c></returns>
        /// <exception cref="NotSupportedException">NotSupportedException</exception>
        public static bool IsDefault(this object source)
        {
            if (source?.GetType().IsValueType == true)
            {
                switch (source)
                {
                    case int s:
                        return s == default;
                    case long s:
                        return s == default;
                    case double s:
                        return s == default;
                    case float s:
                        return s == default;
                    case decimal s:
                        return s == default;
                    case Guid s:
                        return s == default;
                    // etc: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types
                    default:
                        throw new NotSupportedException($"IsDefault for type {source.GetType().Name}");
                }
            }

            return source == null;
        }

        /// <summary>
        /// Check if <see cref="string"/> has a default value
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> when the source has a default value; otherwise, <c>false</c></returns>
        /// <exception cref="NotSupportedException">NotSupportedException</exception>
        public static bool IsDefault(this string source)
        {
            return source == default;
        }

        /// <summary>
        /// Check if <see cref="int"/> has a default value
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> when the source has a default value; otherwise, <c>false</c></returns>
        /// <exception cref="NotSupportedException">NotSupportedException</exception>
        public static bool IsDefault(this int source)
        {
            return source == default;
        }

        /// <summary>
        /// Check if <see cref="Guid"/> has a default value
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> when the source has a default value; otherwise, <c>false</c></returns>
        /// <exception cref="NotSupportedException">NotSupportedException</exception>
        public static bool IsDefault(this Guid source)
        {
            return source == default;
        }

        /// <summary>
        /// Check if <see cref="DateTime"/> has a default value
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> when the source has a default value; otherwise, <c>false</c></returns>
        /// <exception cref="NotSupportedException">NotSupportedException</exception>
        public static bool IsDefault(this DateTime source)
        {
            return source == default;
        }
    }
}
