namespace SSToolkit.Fundamental
{
    using System;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Get pretty name
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Pretty name</returns>
        public static string PrettyName(this Type source)
        {
            if (source.IsGenericType)
            {
                var name = source.Name.Substring(0, source.Name.IndexOf('`'));
                var types = string.Join(",", source.GetGenericArguments()
                                                    .Select(t => t.PrettyName()));
                return $"{name}<{types}>";
            }

            return source.Name;
        }

        /// <summary>
        /// Get Full pretty name
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Full pretty name</returns>
        public static string FullPrettyName(this Type source)
        {
            if (source.IsGenericType)
            {
                var name = source.FullName.Substring(0, source.FullName.IndexOf('`'));
                var types = string.Join(",", source.GetGenericArguments()
                                                    .Select(t => t.FullPrettyName()));
                return $"{name}<{types}>";
            }

            return source.FullName;
        }
    }
}
