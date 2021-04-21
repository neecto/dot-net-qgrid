namespace QGrid.Tests.Db.Postgres.Migrations
{
    public static class PostgresMigrationScript
    {
        public static string GetMigrationScript()
        {
            var sql =
            @"
                DROP TABLE IF EXISTS ""TestItems"";

                CREATE TABLE ""TestItems""
                (
                ""Id"" integer NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                ""IntColumn"" integer NOT NULL,
                ""IntNullableColumn"" integer NULL,
                ""DecimalColumn"" numeric NOT NULL,
                ""DecimalNullableColumn"" numeric NULL,
                ""StringColumn"" varchar NULL,
                ""BoolColumn"" boolean NOT NULL,
                ""BoolNullableColumn"" boolean NULL,
                ""DateTimeColumn"" timestamp NOT NULL,
                ""DateTimeNullableColumn"" timestamp NULL,
                ""EnumColumn"" smallint NOT NULL,
                ""EnumNullableColumn"" smallint NULL,
                ""GuidColumn"" uuid NOT NULL,
                ""GuidNullableColumn"" uuid NULL,
                ""DateTimeOffsetColumn"" timestamp NULL
                );
            ";

            return sql;
        }
    }
}