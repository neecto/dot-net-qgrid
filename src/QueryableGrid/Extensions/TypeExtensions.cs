using System;

namespace QueryableGrid.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsNumberType(this Type type)
            => type == typeof(int) || type == typeof(long) || type == typeof(decimal) || type == typeof(double)
               || type == typeof(int?) || type == typeof(long?) || type == typeof(decimal?) || type == typeof(double?);

        public static bool IsDateTimeType(this Type type)
            => type == typeof(DateTime) || type == typeof(DateTime?);

        public static bool IsNullableType(this Type type)
            => Nullable.GetUnderlyingType(type) != null;
    }
}