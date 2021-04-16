using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class EnumFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public EnumFilterExpressionProvider(
            PropertyInfo memberPropertyInfo,
            QGridFilter filter,
            ParameterExpression entityParameterExpression
        ) : base(memberPropertyInfo, filter, entityParameterExpression)
        {
        }

        protected override Expression GetComparisonExpression(MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            var condition = Filter.Condition;
            switch (condition)
            {
                case FilterConditionEnum.Eq:
                    return Expression.Equal(memberExpression, constantExpression);
                case FilterConditionEnum.Neq:
                    return Expression.NotEqual(memberExpression, constantExpression);
                case FilterConditionEnum.Oneof:
                    return GetOneOfConditionExpression(memberExpression, constantExpression);
                case FilterConditionEnum.Notoneof:
                    return Expression.Not(GetOneOfConditionExpression(memberExpression, constantExpression));
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for enum values"
                    );
            }
        }

        protected override ConstantExpression GetFilterConstantExpression()
        {
            var propertyType = MemberPropertyInfo.PropertyType.GetUnderlyingTypeIfNullable();
            var filterValue = Filter.Value;

            if (Filter.Condition == FilterConditionEnum.Oneof || Filter.Condition == FilterConditionEnum.Notoneof)
            {
                return GetOneOfFilterConstantExpression(filterValue, propertyType);
            }

            try
            {
                var enumValue = GetEnumValue(propertyType, filterValue);
                return Expression.Constant(enumValue);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Failed to convert filter value \"{filterValue}\" to column type {propertyType}",
                    nameof(filterValue),
                    e
                );
            }
        }

        private ConstantExpression GetOneOfFilterConstantExpression(object filterValue, Type propertyType)
        {
            if (!(filterValue is IEnumerable))
            {
                throw new ArgumentException(
                    $"Filter value \"{filterValue}\" is expected to be a collection for 'oneof' filter condition",
                    nameof(filterValue)
                );
            }

            var filterValues = filterValue as IEnumerable;
            var genericListType = typeof(List<>).MakeGenericType(propertyType);
            var convertedFilterValueList = (IList)Activator.CreateInstance(genericListType);

            foreach (var value in filterValues)
            {
                try
                {
                    var enumValue = GetEnumValue(propertyType, value.ToString());

                    convertedFilterValueList.Add(enumValue);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        $"Failed to convert filter collection value \"{value}\" to column type {propertyType}",
                        nameof(filterValue),
                        e
                    );
                }
            }

            return Expression.Constant(convertedFilterValueList);
        }

        private Expression GetOneOfConditionExpression(MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            var containsMethodInfo = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2);

            var propertyType = MemberPropertyInfo.PropertyType.GetUnderlyingTypeIfNullable();

            containsMethodInfo = containsMethodInfo.MakeGenericMethod(propertyType);

            return Expression.Call(null, containsMethodInfo, constantExpression, memberExpression);
        }

        private object GetEnumValue(Type enumType, object value)
        {
            var enumValue = Enum.Parse(enumType, value.ToString(), true);

            // MSDN says that Enum.Parse would throw an OverflowException in case parsed value
            // is outside of Enum range, but for some reason it doesn't happen
            // https://docs.microsoft.com/en-us/dotnet/api/system.enum.parse
            // so we call IsDefined to check this manually
            if (!Enum.IsDefined(enumType, enumValue))
            {
                throw new OverflowException();
            }

            return enumValue;
        }
    }
}