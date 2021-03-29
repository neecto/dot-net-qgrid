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
        public int TotalItems { get; set; }
        private readonly TestDbContext _dbContext;

        public DatabaseFixture()
        {
            _dbContext = new TestDbContext();
            SetupDatabase();

            var list = TestDataGenerator.CreateTestItems();

            _dbContext.TestItems.RemoveRange(_dbContext.TestItems);
            _dbContext.TestItems.AddRange(list);
            _dbContext.SaveChanges();

            TestQueryable = _dbContext.TestItems;
            TotalItems = TestQueryable.Count();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupDatabase()
        {
            var dbReady = false;

            while (!dbReady)
            {
                try
                {
                    if (_dbContext.Database.GetPendingMigrations().Any())
                    {
                        _dbContext.Database.Migrate();
                    }

                    dbReady = true;
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Failed to migrate db with exception: {e.Message}. Will try again in 10 seconds");
                    Thread.Sleep(10 * 1000);
                }
            }
        }
    }
}