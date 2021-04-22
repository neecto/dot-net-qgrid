namespace QGrid.Tests.Migrations
{
    public static class SqlServerMigrationScript
    {
        public static string GetMigrationScript()
        {
            var sql =
                @"
                DROP TABLE IF EXISTS dbo.TestItems;
                CREATE TABLE dbo.TestItems
                (
                    Id INT NOT NULL IDENTITY PRIMARY KEY,
                    IntColumn INT NOT NULL,
                    IntNullableColumn INT NULL,
                    DecimalColumn DECIMAL(10,4) NOT NULL,
                    DecimalNullableColumn DECIMAL(10,4) NULL,
                    StringColumn NVARCHAR(100) NULL,
                    BoolColumn BIT NOT NULL,
                    BoolNullableColumn BIT NULL,
                    DateTimeColumn DATETIME2 NOT NULL,
                    DateTimeNullableColumn DATETIME2 NULL,
                    EnumColumn INT NOT NULL,
                    EnumNullableColumn INT NULL,
                    GuidColumn UNIQUEIDENTIFIER NOT NULL,
                    GuidNullableColumn UNIQUEIDENTIFIER NULL,
                    DateTimeOffsetColumn DATETIMEOFFSET NULL
                );
            ";

            return sql;
        }
    }
}