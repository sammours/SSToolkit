﻿namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Diagnostics;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Returns a <c>Base64</c> encoded <see cref="Guid"/>.
        /// <example>
        /// DRfscsSQbUu8bXRqAvcWQA== or DRfscsSQbUu8bXRqAvcWQA depending on <paramref name="trimEnd"/>.
        /// </example>
        /// <remarks>
        /// The result of this method is not <c>URL</c> safe.
        /// See: <see href="https://blog.codinghorror.com/equipping-our-ascii-armor/"/>
        /// </remarks>
        /// </summary>
        [DebuggerStepThrough]
        public static string ToBase64(this Guid source, bool trimEnd = false)
        {
            var result = Convert.ToBase64String(source.ToByteArray());
            return trimEnd ? result.Substring(0, result.Length - 2) : result; // TODO: optimize https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
        }

        /// <summary>
        /// Generates a maximum of 16 character, <see cref="Guid"/> based string with very little chance of collision.
        /// <example>3c4ebc5f5f2c4edc</example>.
        /// <remarks>
        /// The result of this method is <c>URL</c> safe.
        /// Slower than <see cref="ToBase64"/>.
        /// See: <see href="http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c"/>
        /// </remarks>
        /// </summary>
        [DebuggerStepThrough]
        public static string ToCode(this Guid source)
        {
            long result = 1;
            foreach (var b in source.ToByteArray())
            {
                result *= b + 1;
            }

            return (result - DateTime.Now.Ticks).ToString("x");
        }

        /// <summary>
        /// Generates a 19 character, <see cref="Guid"/> based number.
        /// <example>4801539909457287012</example>.
        /// <remarks>
        /// Faster than <see cref="ToBase64"/>.
        /// See: <see href="http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c"/>
        /// </remarks>
        /// </summary>
        [DebuggerStepThrough]
        public static long ToNumber(this Guid source)
            => BitConverter.ToInt64(source.ToByteArray(), 0);

        [DebuggerStepThrough]
        public static Guid? ToNullableGuid(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return null;
            }

            if (source.IsBase64())
            {
                return new Guid(Convert.FromBase64String(source));
            }

            if (Guid.TryParse(source, out var parsedResult))
            {
                return parsedResult;
            }

            return null;
        }

        /// <summary>
        /// Convert string to Guid
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Guid ToGuid(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return default;
            }

            if (source.IsBase64())
            {
                return new Guid(Convert.FromBase64String(source));
            }

            if (Guid.TryParse(source, out var parsedResult))
            {
                return parsedResult;
            }

            return default;
        }

        /// <summary>
        /// Check if string is valid Guid
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> if valid; otherwise, <c>false</c></returns>
        [DebuggerStepThrough]
        public static bool IsValidGuid(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }


            return Guid.TryParse(source, out var _);
        }

        /// <summary>
        /// Check if object is valid Guid
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> if valid; otherwise, <c>false</c></returns>
        [DebuggerStepThrough]
        public static bool IsValidGuid(this object source)
        {
            if (source == null)
            {
                return false;
            }


            return Guid.TryParse(source.ToString(), out var _);
        }
    }
}
