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
                    IntColumn = 1
                },
                new TestItem
                {
                    IntColumn = 2
                },
                new TestItem
                {
                    IntColumn = 10
                },
                new TestItem
                {
                    IntColumn = 20
                },
                new TestItem
                {
                    IntColumn = 20
                }
            };

            return list;
        }
    }
}