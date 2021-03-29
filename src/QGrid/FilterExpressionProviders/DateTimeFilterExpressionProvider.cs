using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QGrid.Enums;
using QGrid.Models;

namespace QGrid.FilterExpressionProviders
{
    internal class DateTimeFilterExpressionProvider : BaseFilterExpressionProvider
    {
        private readonly FilterConditionEnum[] _dateOnlyConditions =
        {
            FilterConditionEnum.Eqdate,
            FilterConditionEnum.Neqdate,
            FilterConditionEnum.Ltdate,
            FilterConditionEnum.Ltedate,
            FilterConditionEnum.Gtdate,
            FilterConditionEnum.Gtedate
        };

        public DateTimeFilterExpressionProvider(
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
                case FilterConditionEnum.Eqdate:
                    return Expression.Equal(memberExpression, constantExpression);
                case FilterConditionEnum.Neq:
                case FilterConditionEnum.Neqdate:
                    return Expression.NotEqual(memberExpression, constantExpression);
                case FilterConditionEnum.Lt:
                case FilterConditionEnum.Ltdate:
                    return Expression.LessThan(memberExpression, constantExpression);
                case FilterConditionEnum.Gt:
                case FilterConditionEnum.Gtdate:
                    return Expression.GreaterThan(memberExpression, constantExpression);
                case FilterConditionEnum.Lte:
                case FilterConditionEnum.Ltedate:
                    return Expression.LessThanOrEqual(memberExpression, constantExpression);
                case FilterConditionEnum.Gte:
                case FilterConditionEnum.Gtedate:
                    return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(condition),
                        condition,
                        $"Filter condition {Enum.GetName(typeof(FilterConditionEnum), condition)} is not supported for datetime values"
                    );
            }
        }

        protected override MemberExpression GetMemberExpression()
        {
            // if the condition is for the whole DateTime,
            // we can use regular member accessor expression
            if (!_dateOnlyConditions.Contains(Filter.Condition))
            {
                return base.GetMemberExpression();
            }

            // if the condition is for Date part of the DateTime
            // we need to use DateTime.Date property
            var datePropertyInfo = MemberPropertyInfo
                .PropertyType
                .GetProperties()
                .FirstOrDefault(x => x.Name.Equals("Date"));

            var propertyExpression = base.GetMemberExpression();
            var dateExpression = Expression.Property(propertyExpression, datePropertyInfo);

            return dateExpression;
        }

        protected override ConstantExpression GetFilterConstantExpression()
        {
            var filterValue = Filter.Value;

            try
            {
                var dateTimeValue = Convert.ToDateTime(filterValue);

                // if the condition operator is for date only
                // we take the Date part from the filter value
                if (_dateOnlyConditions.Contains(Filter.Condition))
                    return Expression.Constant(dateTimeValue.Date);

                return Expression.Constant(dateTimeValue);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Failed to convert filter value \"{filterValue}\" to column type {nameof(DateTime)}",
                    nameof(filterValue),
                    e
                );
            }
        }
    }
}