using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QGrid.Tests.Models;
using QGrid.Tests.Setup;

namespace QGrid.Tests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public IQueryable<TestItem> TestQueryable { get; }
        private readonly TestDbContext _dbContext;

        public DatabaseFixture()
        {
            _dbContext = new TestDbContext();
            SetupDatabase();

            var list = CreateTestItems();

            _dbContext.TestItems.AddRange(list);
            _dbContext.SaveChanges();

            TestQueryable = _dbContext.TestItems;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupDatabase()
        {
            bool dbReady = false;

            while (!dbReady)
            {
                try
                {
                    _dbContext.Database.Migrate();
                    dbReady = true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Failed to migrate db with exception: {e.Message}. Will try again in 10 seconds");
                    Thread.Sleep(10 * 1000);
                }
            }
        }

        private List<TestItem> CreateTestItems()
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

            return list;
        }
    }
}