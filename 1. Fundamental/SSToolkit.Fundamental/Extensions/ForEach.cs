namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Performs an action on each value of the <see cref="IEnumerable<T>"/>
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="source">The items.</param>
        /// <param name="action">Action to perform on every item</param>
        /// <returns>the source with the actions applied</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            var itemsArray = source as T[] ?? source.ToArray();

            foreach (var value in itemsArray)
            {
                if (action != null && !EqualityComparer<T>.Default.Equals(value, default(T)))
                {
                    action(value);
                }
            }

            return itemsArray;
        }

        /// <summary>
        /// Performs an action on each value of the <see cref="ICollection<T>"/>
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="source">The items.</param>
        /// <param name="action">Action to perform on every item</param>
        /// <returns>the source with the actions applied</returns>
        public static ICollection<T> ForEach<T>(this ICollection<T> source, Action<T> action)
        {
            return source.AsEnumerable().ForEach(action).ToList();
        }

        /// <summary>
        /// Performs an action on each value of the <see cref="IReadOnlyCollection<T>"/>
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="source">The items.</param>
        /// <param name="action">Action to perform on every item</param>
        /// <returns>the source with the actions applied</returns>
        public static IReadOnlyCollection<T> ForEach<T>(this IReadOnlyCollection<T> source, Action<T> action)
        {
            return source.AsEnumerable().ForEach(action).ToList();
        }
    }
}
