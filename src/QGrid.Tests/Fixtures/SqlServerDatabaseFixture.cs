using System;
using System.Linq;
using QGrid.Tests.Models;

namespace QGrid.Tests.Fixtures
{
    public class SqlServerDatabaseFixture : IDisposable
    {
        public IQueryable<TestItem> TestQueryable { get; set; }

        public void Dispose()
        {
        }
    }
}