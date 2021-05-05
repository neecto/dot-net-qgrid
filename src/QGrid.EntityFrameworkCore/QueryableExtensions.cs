using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QGrid.Extensions;
using QGrid.Models;

namespace QGrid.EntityFrameworkCore
{
    public static class QueryableExtensions
    {
        public static async Task<QGridResult<T>> ToQGridResultAsync<T>(this IQueryable<T> query, QGridRequest request)
            where T : class
        {
            var total = await query.CountAsync();
            var resultQuery = query
                .ApplyFilters(request.Filters)
                .ApplyOrdering(request.Ordering);

            var totalFiltered = await resultQuery.CountAsync();
            var skip = request.PageSize * (request.PageNumber - 1);

            var pageResults = await resultQuery
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

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