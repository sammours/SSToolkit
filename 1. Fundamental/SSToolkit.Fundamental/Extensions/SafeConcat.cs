namespace SSToolkit.Fundamental.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Concat <typeparamref name="T"/> to a <see cref="IEnumerable<T>"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="object"></param>
        /// <returns>The <see cref="IEnumerable<T>"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this IEnumerable<T> source, T @object)
        {
            if (source == null)
            {
                return new List<T> { @object };
            }

            return @object != null ? source.Concat(new List<T> { @object }) : source;
        }

        /// <summary>
        /// Concat <see cref="IEnumerable<T>"/> to a <see cref="IEnumerable<T>"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns>The <see cref="IEnumerable<T>"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this IEnumerable<T> source, IEnumerable<T> list)
        {
            if (source == null)
            {
                return list;
            }

            return list != null ? source.Concat(list) : source;
        }

        /// <summary>
        /// Concat <typeparamref name="T"/> to a <see cref="IEnumerable<T>"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="object"></param>
        /// <returns>The <see cref="IEnumerable<T>"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this IList<T> source, T @object)
        {
            if (source == null)
            {
                return new List<T> { @object };
            }

            return @object != null ? source.Concat(new List<T> { @object }) : source;
        }

        /// <summary>
        /// Concat <see cref="IList<T>"/> to a <see cref="IList<T>"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns>The <see cref="IList<T>"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this IList<T> source, IList<T> list)
        {
            if (source == null)
            {
                return list;
            }

            return list != null ? source.Concat(list) : source;
        }

        /// <summary>
        /// Concat <see cref="T[]"/> to an <see cref="T[]"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="array"></param>
        /// <returns>The <see cref="T[]"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this T[] source, T[] array)
        {
            if (source == null)
            {
                return array;
            }

            return array != null ? source.Concat(array) : source;
        }

        /// <summary>
        /// Concat <typeparamref name="T"/> to a <see cref="T[]"/> after null check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="object"></param>
        /// <returns>The <see cref="T[]"/> after concating</returns>
        public static IEnumerable<T> SafeConcat<T>(this T[] source, T obj)
        {
            if (source == null)
            {
                return new List<T>() { obj };
            }

            return obj != null ? source.Concat(new List<T>() { obj }) : source;
        }
    }
}
