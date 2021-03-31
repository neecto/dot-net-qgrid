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
                    DecimalColumn = 1.99m
                },
                new TestItem
                {
                    IntColumn = 2,
                    DecimalColumn = 1.51m
                },
                new TestItem
                {
                    IntColumn = 10,
                    DecimalColumn = 1.85m
                },
                new TestItem
                {
                    IntColumn = 20,
                    DecimalColumn = 20.50m
                },
                new TestItem
                {
                    IntColumn = 20,
                    DecimalColumn = 20.99m
                }
            };

            return list;
        }
    }
}