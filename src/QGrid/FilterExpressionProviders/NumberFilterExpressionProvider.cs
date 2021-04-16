using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class NumberFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public NumberFilterExpressionProvider(
            PropertyInfo memberPropertyInfo,
            QGridFilter filter,
            ParameterExpression entityParameterExpression
        ) : base(memberPropertyInfo, filter, entityParameterExpression)
        {
        }

        protected override Expression GetComparisonExpression(
            MemberExpression memberExpression,
            ConstantExpression constantExpression
        )
        {
            var condition = Filter.Condition;
            switch (condition)
            {
                case FilterConditionEnum.Eq:
                    return Expression.Equal(memberExpression, constantExpression);
                case FilterConditionEnum.Neq:
                    return Expression.NotEqual(memberExpression, constantExpression);
                case FilterConditionEnum.Lt:
                    return Expression.LessThan(memberExpression, constantExpression);
                case FilterConditionEnum.Gt:
                    return Expression.GreaterThan(memberExpression, constantExpression);
                case FilterConditionEnum.Lte:
                    return Expression.LessThanOrEqual(memberExpression, constantExpression);
                case FilterConditionEnum.Gte:
                    return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for number values"
                    );
            }
        }

        protected override ConstantExpression GetFilterConstantExpression()
        {
            var propertyType = MemberPropertyInfo.PropertyType.GetUnderlyingTypeIfNullable();
            var filterValueString = Filter.Value.ToString();
            var tryParseMethod = GetTryParseMethodInfo(propertyType);

            var tryParseResult = (bool)tryParseMethod.Invoke(null, new object[] { filterValueString, null });

            if (tryParseResult == false)
            {
                throw new ArgumentException(
                    $"Failed to convert filter value \"{Filter.Value}\" to column type {propertyType}",
                    nameof(Filter.Value)
                );
            }

            var convertedValue = Convert.ChangeType(Filter.Value, propertyType);
            return Expression.Constant(convertedValue);
        }

        private MethodInfo GetTryParseMethodInfo(Type propertyType) =>
            propertyType.GetMethods()
                .Single(
                    method => method.Name == "TryParse"
                              && method.GetParameters().Length == 2
                              && method.GetParameters().First().ParameterType == typeof(string)
                );
    }
}