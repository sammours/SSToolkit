namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if the string starts with any of the items
        /// </summary>
        /// <param name="source"></param>
        /// <param name="items"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool StartsWithAny(
            this string source,
            IEnumerable<string> items,
            StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (items.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                if (source.StartsWith(item, comp))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
