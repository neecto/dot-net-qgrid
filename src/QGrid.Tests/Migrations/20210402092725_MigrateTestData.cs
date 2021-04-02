using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QGrid.Tests.Migrations
{
    public partial class MigrateTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntColumn = table.Column<int>(nullable: false),
                    IntNullableColumn = table.Column<int>(nullable: true),
                    DecimalColumn = table.Column<decimal>(nullable: false),
                    DecimalNullableColumn = table.Column<decimal>(nullable: true),
                    StringColumn = table.Column<string>(nullable: true),
                    BoolColumn = table.Column<bool>(nullable: false),
                    BoolNullableColumn = table.Column<bool>(nullable: true),
                    DateTimeColumn = table.Column<DateTime>(nullable: false),
                    DateTimeNullableColumn = table.Column<DateTime>(nullable: true),
                    EnumColumn = table.Column<int>(nullable: false),
                    EnumNullableColumn = table.Column<int>(nullable: true),
                    GuidColumn = table.Column<Guid>(nullable: false),
                    GuidNullableColumn = table.Column<Guid>(nullable: true),
                    DateTimeOffsetColumn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestItems");
        }
    }
}
