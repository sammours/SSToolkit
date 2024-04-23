namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Simplifies casting an object to a type.
        /// </summary>
        /// <typeparam name="T">The type to be casted.</typeparam>
        /// <param name="source">The object to cast.</param>
        /// <returns>Casted object.</returns>
        [DebuggerStepThrough]
        public static T As<T>(this object source)
            where T : class
        {
            if (source == null)
            {
                return default;
            }

            try
            {
                return (T)source;
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }

        /// <summary>
        /// Convert object into a list containing this object.
        /// </summary>
        /// <typeparam name="T">The type to be casted.</typeparam>
        /// <param name="source">The object to convert to list.</param>
        /// <returns>List containing the object.</returns>
        [DebuggerStepThrough]
        public static List<T> AsList<T>(this T source)
            where T : class
        {
            if (source == null)
            {
                return default;
            }

            return new List<T> { source };
        }

        /// <summary>
        /// Check type of an object.
        /// </summary>
        /// <typeparam name="T">The type to be checked.</typeparam>
        /// <param name="source">The object to check.</param>
        /// <returns><see langword="true"/> if the object is type of <typeparamref name="T"/>.</returns>
        public static bool Is<T>(this object source)
        {
            if (source == null)
            {
                return false;
            }

            return source is T;
        }
    }
}