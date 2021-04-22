namespace QGrid.Tests.Migrations
{
    public static class MySqlMigrationScript
    {
        public static string GetMigrationScript()
        {
            var sql =
                @"
                    USE qgrid; 

                    DROP TABLE IF EXISTS TestItems;

                    CREATE TABLE TestItems
                    (
                        Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
                        IntColumn INT NOT NULL,
                        IntNullableColumn INT NULL,
                        DecimalColumn DECIMAL(10,4) NOT NULL,
                        DecimalNullableColumn DECIMAL(10,4) NULL,
                        StringColumn VARCHAR(100) NULL,
                        BoolColumn BOOLEAN NOT NULL,
                        BoolNullableColumn BOOLEAN NULL,
                        DateTimeColumn DATETIME NOT NULL,
                        DateTimeNullableColumn DATETIME NULL,
                        EnumColumn INT NOT NULL,
                        EnumNullableColumn INT NULL,
                        GuidColumn CHAR(36) NOT NULL,
                        GuidNullableColumn CHAR(36) NULL,
                        DateTimeOffsetColumn DATETIME NULL
                    );
                ";

            return sql;
        }
    }
}