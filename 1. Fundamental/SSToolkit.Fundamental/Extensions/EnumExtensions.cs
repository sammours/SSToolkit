namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumExtensions
    {
        /// <summary>
        /// Get the <see cref="DescriptionAttribute"/> of an enum as a string
        /// </summary>
        /// <param name="enum"></param>
        /// <returns>Get the <see cref="DescriptionAttribute"/> as a string</returns>
        public static string ToDescription(this Enum @enum)
        {
            var attribute = GetText<DescriptionAttribute>(@enum);
            return attribute.Description;
        }

        /// <summary>
        /// Get the <see cref="Attribute"/> of an enum
        /// </summary>
        /// <typeparam name="T"><see cref="Attribute"/> type</typeparam>
        /// <param name="enum"></param>
        /// <returns>Get the <see cref="Attribute"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetText<T>(Enum @enum)
            where T : Attribute
        {
            var type = @enum.GetType();

            var memberInfo = type.GetMember(@enum.ToString());
            if (memberInfo != null && !memberInfo.Any())
            {
                throw new ArgumentException($"No public members for the argument '{@enum}'.");
            }

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            if (attributes == null || attributes.Length != 1)
            {
                throw new ArgumentException($"Can't find an attribute matching '{typeof(T).Name}' for the argument '{@enum}'");
            }

            return attributes.Single() as T;
        }
    }
}
