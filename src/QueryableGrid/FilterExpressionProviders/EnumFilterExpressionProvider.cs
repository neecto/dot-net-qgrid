using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QueryableGrid.Enums;
using QueryableGrid.Models;

namespace QueryableGrid.FilterExpressionProviders
{
    public class EnumFilterExpressionProvider : BaseFilterExpressionProvider
    {
        public EnumFilterExpressionProvider(
            PropertyInfo memberPropertyInfo,
            ListViewFilter filter,
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
            var propertyType = MemberPropertyInfo.PropertyType;
            var filterValue = Filter.Value;

            if (Filter.Condition == FilterConditionEnum.Oneof || Filter.Condition == FilterConditionEnum.Notoneof)
            {
                return GetOneOfFilterConstantExpression(filterValue, propertyType);
            }

            try
            {
                var enumValue = Enum.Parse(propertyType, filterValue.ToString(), true);
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
            var genericListType = typeof(List<>).MakeGenericType(MemberPropertyInfo.PropertyType);
            var convertedFilterValueList = (IList)Activator.CreateInstance(genericListType);

            foreach (var value in filterValues)
            {
                try
                {
                    convertedFilterValueList.Add(Enum.Parse(propertyType, value.ToString(), true));
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

            containsMethodInfo = containsMethodInfo.MakeGenericMethod(MemberPropertyInfo.PropertyType);

            return Expression.Call(null, containsMethodInfo, constantExpression, memberExpression);
        }
    }
}