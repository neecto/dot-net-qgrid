using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class NullValueFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public NullValueFilterExpressionProvider(
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
                    return GetEqualNullComparisonExpression(memberExpression, constantExpression);
                case FilterConditionEnum.Neq:
                    return Expression.Not(GetEqualNullComparisonExpression(memberExpression, constantExpression));
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for NULL values"
                    );
            }
        }

        protected override ConstantExpression GetFilterConstantExpression() => Expression.Constant(null);

        private Expression GetEqualNullComparisonExpression(MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            // if filtered property is a string we can use regular Expression.Equal
            if (MemberPropertyInfo.PropertyType == typeof(string))
            {
                return Expression.Equal(memberExpression, constantExpression);
            }

            // if filtered property actually is a nullable we can use HasValue method
            if (MemberPropertyInfo.PropertyType.IsNullableType())
            {
                var hasValuePropertyInfo = MemberPropertyInfo.PropertyType
                    .GetProperties()
                    .FirstOrDefault(x => x.Name.Equals("HasValue"));

                var hasValueExpression = Expression.Property(memberExpression, hasValuePropertyInfo);
                var trueConstantExpression = Expression.Constant(false);

                // x => x.[NullableProperty].HasValue == false
                return Expression.Equal(hasValueExpression, trueConstantExpression);
            }

            // otherwise, for Equal condition on not nullable columns we should always return FALSE
            // because Linq cannot translate equal expression for
            // not nullable column and null value
            var stubConstantExpression = Expression.Constant(false);
            return Expression.IsTrue(stubConstantExpression);
        }
    }
}