using System;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class BoolFilterExpressionsProvider : BaseFilterExpressionProvider
    {
        public BoolFilterExpressionsProvider(
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
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for boolean values"
                    );
            }
        }
    }
}