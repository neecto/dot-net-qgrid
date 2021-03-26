using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using QGrid.Tests.Models;

namespace QGrid.Tests.Setup
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestItem> TestItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbServer = Environment.GetEnvironmentVariable("DBSERVER");
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            Console.WriteLine($"Setting up EF DB context for {dbServer}.");
            Console.WriteLine($"using connection string: {connectionString}");

            if (dbServer == "MSSQL")
            {
                ConfigureMsSql(optionsBuilder, connectionString);
            }
            else
            {
                Console.WriteLine("Failed to find DB configuration in environment variables, falling back to hard-coded config");
                optionsBuilder
                    .UseSqlServer(
                        "Server=.;Database=qgrid;User=sa;Password=123QGridTest!@#;",
                        x => {
                            x.MigrationsAssembly(typeof(TestDbContext).GetTypeInfo().Assembly.GetName().Name);
                        }
                    );
            }
        }

        private void ConfigureMsSql(DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseSqlServer(
                    connectionString,
                    x => {
                        x.MigrationsAssembly(typeof(TestDbContext).GetTypeInfo().Assembly.GetName().Name);
                    }
                );
        }
    }
}