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
                    DecimalNullableColumn = 9.99m
                },
                new TestItem
                {
                    IntColumn = 2,
                    IntNullableColumn = 5,
                    DecimalColumn = 1.51m,
                    DecimalNullableColumn = null
                },
                new TestItem
                {
                    IntColumn = 10,
                    IntNullableColumn = 6,
                    DecimalColumn = 1.85m,
                    DecimalNullableColumn = null

                },
                new TestItem
                {
                    IntColumn = 20,
                    IntNullableColumn = null,
                    DecimalColumn = 20.50m,
                    DecimalNullableColumn = 21.55m
                },
                new TestItem
                {
                    IntColumn = 20,
                    IntNullableColumn = 8,
                    DecimalColumn = 20.99m,
                    DecimalNullableColumn = 20.98m
                }
            };

            return list;
        }
    }
}