using System;
using System.Collections.Generic;
using System.Linq;
using QGrid.Tests.Models;

namespace QGrid.Tests.Fixtures
{
    public class SqlServerDatabaseFixture : IDisposable
    {
        public IQueryable<TestItem> TestQueryable { get; }

        public SqlServerDatabaseFixture()
        {
            var list = new List<TestItem>
            {
                new TestItem
                {
                    IntColumn = 1
                },
                new TestItem
                {
                    IntColumn = 2
                }
            };

            TestQueryable = list.AsQueryable();
        }

        public void Dispose()
        {
        }
    }
}