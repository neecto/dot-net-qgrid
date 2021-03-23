using System;
using System.Linq;
using System.Reflection;

namespace QGrid.Extensions
{
    internal static class StringExtensions
    {
        public static PropertyInfo GetPropertyInfo<T>(this string propertyName)
        {
            var propertyInfo = typeof(T)
                .GetProperties()
                .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    $"Failed to find property by name {propertyName} in {typeof(T).Name} object",
                    nameof(propertyName)
                );
            }

            return propertyInfo;
        }
    }
}