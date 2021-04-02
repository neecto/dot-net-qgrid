using System;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Extensions;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal abstract class BaseFilterExpressionProvider
    {
        protected readonly QGridFilter Filter;
        protected readonly PropertyInfo MemberPropertyInfo;
        private protected readonly ParameterExpression EntityParameterExpression;

        protected BaseFilterExpressionProvider(
            PropertyInfo memberPropertyInfo,
            QGridFilter filter,
            ParameterExpression entityParameterExpression
        )
        {
            Filter = filter;
            MemberPropertyInfo = memberPropertyInfo;
            EntityParameterExpression = entityParameterExpression;
        }

        public virtual Expression GetFilterExpression()
        {
            var memberExpression = GetMemberExpression();
            var constantExpression = GetFilterConstantExpression();

            var filterExpression = GetComparisonExpression(
                memberExpression,
                constantExpression
            );

            return filterExpression;
        }

        protected abstract Expression GetComparisonExpression(
            MemberExpression memberExpression,
            ConstantExpression constantExpression
        );

        protected virtual MemberExpression GetMemberExpression()
        {
            var memberExpression = Expression.Property(EntityParameterExpression, MemberPropertyInfo);

            // if filtered value is not nullable or is we are going to use NullValueFilterProvider
            // use base member expression
            if (!MemberPropertyInfo.PropertyType.IsNullableType() || Filter.Value == null)
            {
                return memberExpression;
            }

            // otherwise, get member expression for "Value" property of a nullable type
            var valuePropertyInfo = MemberPropertyInfo.PropertyType.GetProperty("Value");
            return Expression.Property(memberExpression, valuePropertyInfo);
        }

        protected virtual ConstantExpression GetFilterConstantExpression()
        {
            var propertyType = MemberPropertyInfo.PropertyType;
            var filterValue = Filter.Value;

            try
            {
                var convertedValue = Convert.ChangeType(filterValue, propertyType);
                return Expression.Constant(convertedValue);
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
    }
}