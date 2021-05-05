using System.Collections.Generic;
using QGrid.EntityFrameworkCore;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.QGridResultTests
{
    [Collection("Database collection")]
    public class QGridResultsEFCoreTests
    {
        private readonly DatabaseFixture _fixture;

        public QGridResultsEFCoreTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void QGridResult_NoFilter_LargePage_WithResults()
        {
            var pageSize = 10;
            var pageNumber = 1;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.NotEmpty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(result.Items.Count, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.Equal(result.Total, result.TotalFiltered); // no filters
            Assert.Equal(1, result.PagesTotal);
            Assert.True(result.ItemsOnPage < pageSize);
        }

        [Fact]
        public void QGridResult_NoFilter_LargePage_NoResults()
        {
            var pageSize = 10;
            var pageNumber = 2;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.Empty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(0, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.Equal(result.Total, result.TotalFiltered); // no filters
            Assert.Equal(1, result.PagesTotal);
        }

        [Fact]
        public void QGridResult_NoFilter_SmallPage_WithResults()
        {
            var pageSize = 2;
            var pageNumber = 1;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.NotEmpty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(result.Items.Count, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.Equal(result.Total, result.TotalFiltered); // no filters
            Assert.Equal(3, result.PagesTotal);
            Assert.True(result.ItemsOnPage == pageSize);
        }

        [Fact]
        public void QGridResult_Filter_LargePage_WithResults()
        {
            var pageSize = 10;
            var pageNumber = 1;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                Filters = new QGridFilters
                {
                    Operator = FilterOperatorEnum.And,
                    Filters = new List<QGridFilter>
                    {
                        new QGridFilter("EnumColumn", FilterConditionEnum.Eq, TestEnum.Second),
                        new QGridFilter("DecimalColumn", FilterConditionEnum.Gt, 10)
                    }
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.NotEmpty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(result.Items.Count, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.True(result.Total > result.TotalFiltered);
            Assert.Equal(1, result.PagesTotal);
            Assert.True(result.ItemsOnPage < pageSize);
        }

        [Fact]
        public void QGridResult_Filter_SmallPage_WithResults()
        {
            var pageSize = 2;
            var pageNumber = 1;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                Filters = new QGridFilters
                {
                    Operator = FilterOperatorEnum.And,
                    Filters = new List<QGridFilter>
                    {
                        new QGridFilter("EnumColumn", FilterConditionEnum.Eq, TestEnum.Second)
                    }
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.NotEmpty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(result.Items.Count, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.True(result.Total > result.TotalFiltered);
            Assert.True(result.ItemsOnPage < result.TotalFiltered);
            Assert.Equal(2, result.PagesTotal);
            Assert.Equal(pageSize, result.ItemsOnPage);
        }

        [Fact]
        public void QGridResult_Filter_SmallPage_NoResults()
        {
            var pageSize = 2;
            var pageNumber = 1;

            var request = new QGridRequest
            {
                Ordering = new List<QGridOrder>
                {
                    new QGridOrder("DecimalNullableColumn")
                },
                Filters = new QGridFilters
                {
                    Operator = FilterOperatorEnum.And,
                    Filters = new List<QGridFilter>
                    {
                        new QGridFilter("EnumColumn", FilterConditionEnum.Eq, TestEnum.Second),
                        new QGridFilter("EnumColumn", FilterConditionEnum.Eq, TestEnum.First),
                    }
                },
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = _fixture.TestQueryable
                .ToQGridResultAsync(request)
                .Result;

            Assert.Empty(result.Items);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(result.Items.Count, result.ItemsOnPage);
            Assert.Equal(_fixture.TotalItems, result.Total);
            Assert.Equal(0, result.TotalFiltered);
            Assert.Equal(0, result.ItemsOnPage);
            Assert.Equal(0, result.PagesTotal);
            Assert.Equal(0, result.ItemsOnPage);
        }
    }
}