using System.Collections.Generic;
using System.Linq;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;

namespace QGrid.Tests.FilterTests
{
    public abstract class BaseFilterTests
    {
        protected readonly DatabaseFixture Fixture;

        protected BaseFilterTests(DatabaseFixture fixture)
        {
            Fixture = fixture;
        }

        protected List<TestItem> ExecuteQuery(QGridFilters filters) =>
            Fixture.TestQueryable
                .ApplyFilters(filters)
                .ToList();
    }
}