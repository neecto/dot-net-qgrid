using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Models;
using QGrid.Tests.Fixtures;

namespace QGrid.Tests.FilterTests
{
    public class BaseFilterProviderTests : BaseFilterTests
    {
        private readonly string _filterTestColumn;

        public BaseFilterProviderTests(DatabaseFixture fixture, string testColumn) : base(fixture)
        {
            _filterTestColumn = testColumn;
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
    }
}