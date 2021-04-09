using System;
using System.Collections.Generic;
using QGrid.Tests.Models;

namespace QGrid.Tests.Setup
{
    public static class TestDataGenerator
    {
        public static List<TestItem> CreateTestItems()
        {
            var list = new List<TestItem>
            {
                new TestItem
                {
                    IntColumn = 1,
                    IntNullableColumn = null,
                    DecimalColumn = 1.99m,
                    DecimalNullableColumn = 9.99m,
                    StringColumn = null,
                    BoolColumn = true,
                    BoolNullableColumn = null,
                    DateTimeColumn = new DateTime(2021, 1, 5, 12, 12, 54)
                },
                new TestItem
                {
                    IntColumn = 2,
                    IntNullableColumn = 5,
                    DecimalColumn = 1.51m,
                    DecimalNullableColumn = null,
                    StringColumn = "This is a string",
                    BoolColumn = true,
                    BoolNullableColumn = false,
                    DateTimeColumn = new DateTime(2021, 1, 5, 12, 12, 56)
                },
                new TestItem
                {
                    IntColumn = 10,
                    IntNullableColumn = 6,
                    DecimalColumn = 1.85m,
                    DecimalNullableColumn = null,
                    StringColumn = "case invariant?",
                    BoolColumn = true,
                    BoolNullableColumn = true,
                    DateTimeColumn = new DateTime(2021, 1, 5, 18, 0, 0)
                },
                new TestItem
                {
                    IntColumn = 20,
                    IntNullableColumn = null,
                    DecimalColumn = 20.50m,
                    DecimalNullableColumn = 21.55m,
                    StringColumn = "Case Invariant?",
                    BoolColumn = false,
                    BoolNullableColumn = true,
                    DateTimeColumn = new DateTime(2021, 4, 5, 14, 10, 53)
                },
                new TestItem
                {
                    IntColumn = 20,
                    IntNullableColumn = 8,
                    DecimalColumn = 20.99m,
                    DecimalNullableColumn = 20.98m,
                    StringColumn = null,
                    BoolColumn = false,
                    BoolNullableColumn = null,
                    DateTimeColumn = new DateTime(2021, 4, 5, 15, 15, 15)
                }
            };

            return list;
        }
    }
}