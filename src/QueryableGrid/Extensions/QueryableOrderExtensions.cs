using System;
using System.Linq;
using System.Linq.Expressions;
using QueryableGrid.Enums;
using QueryableGrid.Models;

namespace QueryableGrid.Extensions
{
    public static class QueryableOrderExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ListViewOrder orderBy, ListViewOrder thenBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy?.Column))
                return query;

            IOrderedQueryable<T> orderedQuery;

            if (orderBy.Type == OrderTypeEnum.Asc)
            {
                orderedQuery = query
                    .Order(orderBy.Column, "OrderBy");
            }
            else
            {
                orderedQuery = query
                    .Order(orderBy.Column, "OrderByDescending");
            }

            if (string.IsNullOrWhiteSpace(thenBy?.Column))
                return orderedQuery;

            if (thenBy.Type == OrderTypeEnum.Asc)
            {
                orderedQuery = orderedQuery
                    .Order(thenBy.Column, "ThenBy");
            }
            else
            {
                orderedQuery = orderedQuery
                    .Order(thenBy.Column, "ThenByDescending");
            }

            return orderedQuery;
        }

        private static IOrderedQueryable<T> Order<T>(
            this IQueryable<T> query,
            string column,
            string methodName
        )
        {
            var parameterExpression = Expression.Parameter(typeof(T), typeof(T).Name);
            var propertyInfo = column.GetPropertyInfo<T>();
            var memberExpression = Expression.Property(parameterExpression, propertyInfo);

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyInfo.PropertyType);
            var orderingMethod = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

            var lambda = Expression.Lambda(delegateType, memberExpression, parameterExpression);

            var invocationResult = orderingMethod
                .Invoke(null, new object[] { query, lambda });

            return (IOrderedQueryable<T>)invocationResult;
        }
    }
}