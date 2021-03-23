using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    public class NumberFilterTests : IClassFixture<SqlServerDatabaseFixture>
    {
        private readonly SqlServerDatabaseFixture _fixture;

        public NumberFilterTests(SqlServerDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Eq_OneFilter()
        {
            var filter = new List<ListViewFilter>
            {
                new ListViewFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Eq,
                    Value = 1
                }
            };

            var result = _fixture.TestQueryable
                .ApplyFilters<TestItem>(filter)
                .ToList();
        }
    }
}