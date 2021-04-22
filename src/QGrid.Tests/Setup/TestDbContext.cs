using System;
using Microsoft.EntityFrameworkCore;
using QGrid.Tests.Models;

namespace QGrid.Tests.Setup
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestItem> TestItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbServer = DbConfiguration.GetDbServer();
            var connectionString = DbConfiguration.GetConnectionString();

            Console.WriteLine($"Setting up EF DB context for {dbServer}.");
            Console.WriteLine($"using connection string: {connectionString}");

            switch (dbServer)
            {
                case DbConfiguration.MsSql:
                    optionsBuilder.ConfigureSqlServer(connectionString);
                    break;
                case DbConfiguration.Postgres:
                    optionsBuilder.ConfigurePostgres(connectionString);
                    break;
                case DbConfiguration.MySql:
                    optionsBuilder.ConfigureMySql(connectionString);
                    break;
                default:
                    connectionString = "Server=.;Database=qgrid;User=sa;Password=123QGridTest!@#;";
                    Console.WriteLine("Failed to find DB configuration in environment variables, falling back to hard-coded config");
                    optionsBuilder.ConfigureSqlServer(connectionString);
                    break;
            }
        }
    }
}