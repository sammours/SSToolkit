﻿namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Finds the index of the first item matching an expression in an enumerable.
        /// </summary>
        /// <typeparam name="T">the item type</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="predicate">The expression to test the items against.</param>
        /// <returns>
        /// The index of the first matching item, or -1 if no items match.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// items
        /// or
        /// predicate
        /// </exception>
        public static int PositionOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int index = 0;
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Finds the index of the first occurence of an item in an enumerable.
        /// </summary>
        /// <typeparam name="T">the item type</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>
        /// The index of the first matching item, or -1 if the item was not found.
        /// </returns>
        public static int PositionOf<T>(this IEnumerable<T> items, T item)
        {
            return items.PositionOf(i => EqualityComparer<T>.Default.Equals(item, i));
        }
    }
}
