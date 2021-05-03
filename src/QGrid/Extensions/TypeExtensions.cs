using System;

namespace QGrid.Extensions
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

        public static bool IsBoolType(this Type type)
            => type == typeof(bool) || type == typeof(bool?);

        public static bool IsGuidType(this Type type)
            => type == typeof(Guid) || type == typeof(Guid?);

        public static bool IsEnum(this Type type)
        {
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);

            if (nullableUnderlyingType == null)
            {
                return type.IsEnum;
            }

            return nullableUnderlyingType.IsEnum;
        }

        public static Type GetUnderlyingTypeIfNullable(this Type type)
            => type.IsNullableType()
                ? Nullable.GetUnderlyingType(type)
                : type;
    }
}