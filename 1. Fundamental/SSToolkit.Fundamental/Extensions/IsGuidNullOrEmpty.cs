namespace SSToolkit.Fundamental.Extensions
{
    using System;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Check if Guid is null or empty
        /// </summary>
        /// <param name="source">the original guid</param >
        /// <returns>true when Guid null or empty</returns>
        public static bool IsGuidNullOrEmpty(this Guid source)
        {
            return source == Guid.Empty;
        }

        /// <summary>
        /// Check if Guid is null or empty
        /// </summary>
        /// <param name="source">the original guid</param >
        /// <returns>true when Guid null or empty</returns>
        public static bool IsGuidNullOrEmpty(this Guid? source)
        {
            return source.HasValue ? source.Value.IsGuidNullOrEmpty() : true;
        }
    }
}
