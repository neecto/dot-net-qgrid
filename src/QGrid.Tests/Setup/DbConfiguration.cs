using Microsoft.EntityFrameworkCore;

namespace QGrid.Tests.Setup
{
    public static class DbConfiguration
    {
        public static void ConfigurePostgres(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseNpgsql(connectionString);
        }

        public static void ConfigureSqlServer(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder
                .UseSqlServer(
                    connectionString
                );
        }
    }
}