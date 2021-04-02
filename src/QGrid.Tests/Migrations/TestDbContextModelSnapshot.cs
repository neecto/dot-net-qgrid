﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QGrid.Tests.Setup;

namespace QGrid.Tests.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QGrid.Tests.Models.TestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BoolColumn")
                        .HasColumnType("bit");

                    b.Property<bool?>("BoolNullableColumn")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateTimeColumn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTimeNullableColumn")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("DateTimeOffsetColumn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("DecimalColumn")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DecimalNullableColumn")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EnumColumn")
                        .HasColumnType("int");

                    b.Property<int?>("EnumNullableColumn")
                        .HasColumnType("int");

                    b.Property<Guid>("GuidColumn")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GuidNullableColumn")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IntColumn")
                        .HasColumnType("int");

                    b.Property<int?>("IntNullableColumn")
                        .HasColumnType("int");

                    b.Property<string>("StringColumn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TestItems");
                });
#pragma warning restore 612, 618
        }
    }
}
