using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;

namespace QGrid.Tests.FilterTests
{
    public abstract class BaseFilterTests
    {
        protected readonly DatabaseFixture Fixture;
        private readonly string _filterTestColumn;

        protected BaseFilterTests(DatabaseFixture fixture, string testColumn)
        {
            _filterTestColumn = testColumn;
            Fixture = fixture;
        }

        protected QGridFilters CreateQGridFilters(
            FilterConditionEnum condition,
            object value,
            FilterOperatorEnum op = FilterOperatorEnum.And
        )
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter(_filterTestColumn, condition, value)
            };

            return new QGridFilters(op, filters);
        }

        protected QGridFilters CreateQGridFilters(
            FilterConditionEnum condition,
            List<object> values,
            FilterOperatorEnum op = FilterOperatorEnum.And
        )
        {
            var filters = values
                .Select(x => new QGridFilter(_filterTestColumn, condition, x))
                .ToList();
            return new QGridFilters(op, filters);
        }

        protected List<TestItem> ExecuteQuery(QGridFilters filters) =>
            Fixture.TestQueryable
                .ApplyFilters(filters)
                .ToList();
    }
}