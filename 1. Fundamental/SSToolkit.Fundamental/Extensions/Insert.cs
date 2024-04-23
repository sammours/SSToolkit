namespace SSToolkit.Fundamental.Extensions
{
    using System.Collections.Generic;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Also adds the item to the result list.
        /// </summary>
        /// <typeparam name="TSource">the source</typeparam>
        /// <param name="source">the source collection</param>
        /// <param name="item">The item to add to the result</param>
        /// <param name="index">the index at which the item should inserted</param>
        /// <returns></returns>
        public static List<TSource> Insert<TSource>(this List<TSource> source, TSource item, int index = 0)
        {
            if (item == null)
            {
                return source;
            }

            if (source == null)
            {
                return new List<TSource>
                {
                    item
                };
            }

            if (index > 0)
            {
                source.Insert(index, item);
            }
            else
            {
                source.Add(item);
            }

            return source;
        }
    }
}
