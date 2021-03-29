using System;
using System.Linq;
using QGrid.Models;

namespace QGrid.Extensions
{
    public static class QueryableExtensions
    {
        public static QGridResult<T> ToQGridResult<T>(this IQueryable<T> query, QGridRequest request)
            where T : class
        {
            var total = query.Count();
            var resultQuery = query
                .ApplyFilters(request.Filters)
                .ApplyOrdering(request.Ordering);

            var totalFiltered = resultQuery.Count();
            var skip = request.PageSize * (request.PageNumber - 1);

            var pageResults = resultQuery
                .Skip(skip)
                .Take(request.PageSize)
                .ToList();

            var pagesTotal = (int)Math.Ceiling((double)totalFiltered / (double)request.PageSize);

            return new QGridResult<T>
            {
                Items = pageResults,
                ItemsOnPage = pageResults.Count,
                PageNumber = request.PageNumber,
                PagesTotal = pagesTotal,
                Total = total,
                TotalFiltered = totalFiltered
            };
        }
    }
}