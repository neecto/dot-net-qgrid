using System;
using System.Linq.Expressions;
using System.Reflection;
using QueryableGrid.Enums;
using QueryableGrid.Models;

namespace QueryableGrid.FilterExpressionProviders
{
    public class NumberFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public NumberFilterExpressionProvider(
            PropertyInfo memberPropertyInfo,
            ListViewFilter filter,
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
    }
}