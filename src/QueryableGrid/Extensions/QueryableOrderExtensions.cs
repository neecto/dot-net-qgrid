using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using QueryableGrid.Enums;
using QueryableGrid.Models;

namespace QueryableGrid.Extensions
{
    public static class QueryableOrderExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IList<ListViewOrder> ordering)
        {
            if (ordering == null || ordering.Count == 0)
                return query;

            IOrderedQueryable<T> orderedQuery = null;

            foreach (var order in ordering)
            {
                if (orderedQuery == null)
                {
                    var orderMethod = order.Type == OrderTypeEnum.Asc
                        ? "OrderBy"
                        : "OrderByDescending";

                    orderedQuery = query.Order(order.Column, orderMethod);
                }
                else
                {
                    var orderMethod = order.Type == OrderTypeEnum.Asc
                        ? "ThenBy"
                        : "ThenByDescending";

                    orderedQuery = orderedQuery.Order(order.Column, orderMethod);
                }
            }

            return orderedQuery;
        }

        private static IOrderedQueryable<T> Order<T>(
            this IQueryable<T> query,
            string column,
            string methodName
        )
        {
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentNullException(nameof(column), "Column name cannot be empty for ordering");
            }

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