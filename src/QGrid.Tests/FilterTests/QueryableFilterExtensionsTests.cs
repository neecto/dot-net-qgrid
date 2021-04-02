using System;
using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class QueryableFilterExtensionsTests
    {
        private readonly DatabaseFixture _fixture;

        public QueryableFilterExtensionsTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void NullFilter_NoError()
        {
            var result = _fixture.TestQueryable
                .ApplyFilters(null)
                .ToList();

            Assert.NotEmpty(result);
            Assert.True(result.Count == _fixture.TotalItems);
        }

        [Fact]
        public void NullFilters_NoError()
        {
            var result = _fixture.TestQueryable
                .ApplyFilters(new QGridFilters())
                .ToList();

            Assert.NotEmpty(result);
            Assert.True(result.Count == _fixture.TotalItems);
        }

        [Fact]
        public void EmptyFilters_NoError()
        {
            var result = _fixture.TestQueryable
                .ApplyFilters(new QGridFilters(FilterOperatorEnum.And, new List<QGridFilter>()))
                .ToList();

            Assert.NotEmpty(result);
            Assert.True(result.Count == _fixture.TotalItems);
        }

        [Fact]
        public void InvalidProperty_ThrowsArgumentException()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("InvalidColumnName", FilterConditionEnum.Contains, "test")
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _fixture.TestQueryable
                    .ApplyFilters(new QGridFilters(FilterOperatorEnum.And, filters))
                    .ToList();
            });
        }

        [Fact]
        public void UnsupportedPropertyType_ThrowsArgumentOutOfRangeException()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("DateTimeOffsetColumn", FilterConditionEnum.Eq, "test")
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                _fixture.TestQueryable
                    .ApplyFilters(new QGridFilters(FilterOperatorEnum.And, filters))
                    .ToList();
            });
        }
    }
}