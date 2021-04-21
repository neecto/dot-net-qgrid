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
            var dbServer = Environment.GetEnvironmentVariable("DBSERVER");
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            Console.WriteLine($"Setting up EF DB context for {dbServer}.");
            Console.WriteLine($"using connection string: {connectionString}");

            switch (dbServer)
            {
                case "MSSQL":
                    optionsBuilder.ConfigureSqlServer(connectionString);
                    break;
                case "POSTGRES":
                    optionsBuilder.ConfigurePostgres(connectionString);
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