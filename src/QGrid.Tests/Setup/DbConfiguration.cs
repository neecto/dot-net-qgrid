using System;
using Microsoft.EntityFrameworkCore;

namespace QGrid.Tests.Setup
{
    public static class DbConfiguration
    {
        public const string MsSql = "MSSQL";
        public const string Postgres = "POSTGRES";
        public const string MySql = "MYSQL";

        public static string GetDbServer()
            => Environment.GetEnvironmentVariable("DBSERVER") == null
                ? "MSSQL"
                : Environment.GetEnvironmentVariable("DBSERVER");

        public static string GetConnectionString()
            => Environment.GetEnvironmentVariable("CONNECTION_STRING") == null
                ? "Server=.;Database=master;User=sa;Password=123QGridTest!@#;"
                : Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public static bool IsDbCaseSensitive()
            => Environment.GetEnvironmentVariable("DBSERVER") == Postgres;


        public static void ConfigurePostgres(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseNpgsql(connectionString);
        }

        public static void ConfigureSqlServer(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseSqlServer(connectionString);
        }

        public static void ConfigureMySql(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseMySql(connectionString);
        }
    }
}