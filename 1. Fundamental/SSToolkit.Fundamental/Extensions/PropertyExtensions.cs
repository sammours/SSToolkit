namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Get property value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="propertyName">The property name</param>
        /// <returns><c>Value</c> if property has been found and has a value; otherwise, <c>string.Empty</c></returns>
        public static string GetPropertyValue<T>(this T entity, string propertyName)
        {
            if (propertyName.IsNullOrEmpty())
            {
                return string.Empty;
            }

            if (entity == null)
            {
                return string.Empty;
            }

            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == propertyName)
                {
                    return property.GetValue(entity)?.ToString() ?? string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get property value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="propertyName">The property name</param>
        /// <returns><c>Value</c> if property has been found and has a value; otherwise, <c>default</c></returns>
        public static TProperty GetPropertyValue<T, TProperty>(this T entity, string propertyName)
        {
            if (propertyName.IsNullOrEmpty())
            {
                return default;
            }

            if (entity == null)
            {
                return default;
            }

            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == propertyName)
                {
                    var value = property.GetValue(entity);
                    return value != null ? (TProperty)value : default;
                }
            }

            return default;
        }

        /// <summary>
        /// Get property value by expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="expression">The expression </param>
        /// <returns><c>Value</c> if property has been found and has a value; otherwise, <c>default</c></returns>
        public static TProperty GetPropertyValue<T, TProperty>(this T entity, Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
            {
                return default;
            }

            if (entity == null)
            {
                return default;
            }

            var fieldSelector = expression.Compile();
            var value = fieldSelector(entity).ToString();
            return value != null ? (TProperty)Convert.ChangeType(value, typeof(TProperty)) : default;
        }
    }
}
