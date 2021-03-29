using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class StringFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public StringFilterExpressionProvider(
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
                case FilterConditionEnum.Contains:
                    return Expression.Call(memberExpression, ContainsMethod, constantExpression);
                case FilterConditionEnum.Doesnotcontain:
                    return Expression.Not(Expression.Call(memberExpression, ContainsMethod, constantExpression));
                case FilterConditionEnum.Startswith:
                    return Expression.Call(memberExpression, StartsWithMethod, constantExpression);
                case FilterConditionEnum.Endswith:
                    return Expression.Call(memberExpression, EndsWithMethod, constantExpression);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for string values"
                    );
            }
        }

        private static readonly MethodInfo ContainsMethod = typeof(string)
            .GetMethods()
            .First(m => m.Name == "Contains" && m.GetParameters().Length == 1);

        private static readonly MethodInfo StartsWithMethod = typeof(string)
            .GetMethods()
            .First(m => m.Name == "StartsWith" && m.GetParameters().Length == 1);

        private static readonly MethodInfo EndsWithMethod = typeof(string)
            .GetMethods()
            .First(m => m.Name == "EndsWith" && m.GetParameters().Length == 1);
    }
}