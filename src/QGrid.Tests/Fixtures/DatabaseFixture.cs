using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using QGrid.Tests.Migrations;
using QGrid.Tests.Models;
using QGrid.Tests.Setup;

namespace QGrid.Tests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public IQueryable<TestItem> TestQueryable { get; }
        public int TotalItems { get; }
        public bool IsCaseSensitive { get; }
        private readonly TestDbContext _dbContext;

        public DatabaseFixture()
        {
            _dbContext = new TestDbContext();
            SetupDatabase();

            var list = TestDataGenerator.GetTestRecords();

            _dbContext.TestItems.RemoveRange(_dbContext.TestItems);
            _dbContext.TestItems.AddRange(list);
            _dbContext.SaveChanges();

            TestQueryable = _dbContext.TestItems;
            TotalItems = TestQueryable.Count();
            IsCaseSensitive = DbConfiguration.IsDbCaseSensitive();
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
                    ApplyMigrationScript();
                    dbReady = true;
                }
                catch (DbException e)
                {
                    Console.WriteLine("Failed to migrate db with exception. Will try again in 10 seconds");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(e.InnerException?.ToString());
                    Console.WriteLine(e.InnerException?.InnerException?.ToString());
                    Thread.Sleep(10 * 1000);
                }
            }
        }

        private void ApplyMigrationScript()
        {
            var dbServer = DbConfiguration.GetDbServer();
            string migrationSql;

            switch (dbServer)
            {
                case DbConfiguration.MsSql:
                    migrationSql = SqlServerMigrationScript.GetMigrationScript();
                    break;
                case DbConfiguration.Postgres:
                    migrationSql = PostgresMigrationScript.GetMigrationScript();
                    break;
                case DbConfiguration.MySql:
                    migrationSql = MySqlMigrationScript.GetMigrationScript();
                    break;
                default:
                    migrationSql = SqlServerMigrationScript.GetMigrationScript();
                    break;
            }

            _dbContext.Database.ExecuteSqlRaw(migrationSql);
        }
    }
}