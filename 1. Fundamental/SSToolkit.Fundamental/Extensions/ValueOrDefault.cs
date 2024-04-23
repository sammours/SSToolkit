namespace SSToolkit.Fundamental.Extensions
{
    using System.Collections.Generic;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Get value or default of dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T ValueOrDefault<T>(this Dictionary<string, T> source, string key, T @default = default)
        {
            if (source.IsNullOrEmpty() || key.IsNullOrEmpty())
            {
                return @default;
            }

            if (source.TryGetValue(key, out T result))
            {
                return result;
            }

            return @default;
        }

        /// <summary>
        /// Get value or default of dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ValueOrDefault<T>(this Dictionary<string, T> source, string key)
        {
            if (source.IsNullOrEmpty() || key.IsNullOrEmpty())
            {
                return default;
            }

            if (source.TryGetValue(key, out T result))
            {
                return result;
            }

            return default;
        }
    }
}
