using System;
using System.Collections.Generic;
using QGrid.Tests.Models;

namespace QGrid.Tests.Setup
{
    public static class TestDataGenerator
    {
        public static List<TestItem> GetTestRecords()
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
                    DateTimeColumn = new DateTime(2021, 1, 5, 12, 12, 54),
                    DateTimeNullableColumn = null,
                    EnumColumn = TestEnum.First,
                    EnumNullableColumn = null,
                    GuidColumn = Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011"),
                    GuidNullableColumn = null
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
                    DateTimeColumn = new DateTime(2021, 1, 5, 12, 12, 56),
                    DateTimeNullableColumn = new DateTime(2021, 1, 1, 12, 12, 12),
                    EnumColumn = TestEnum.Second,
                    EnumNullableColumn = TestEnum.Nineth,
                    GuidColumn = Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011"),
                    GuidNullableColumn = null
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
                    DateTimeColumn = new DateTime(2021, 1, 5, 18, 0, 0),
                    DateTimeNullableColumn = new DateTime(2021, 1, 1, 13, 13, 13),
                    EnumColumn = TestEnum.Third,
                    EnumNullableColumn = TestEnum.Nineth,
                    GuidColumn = Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011"),
                    GuidNullableColumn = Guid.Parse("6D1EE7D6-F47B-45B5-AEB3-1B633AD730BD")
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
                    DateTimeColumn = new DateTime(2021, 4, 5, 14, 10, 53),
                    DateTimeNullableColumn = null,
                    EnumColumn = TestEnum.First,
                    EnumNullableColumn = null,
                    GuidColumn = Guid.Parse("A4E69C0C-75EB-47E9-BC0D-21308D91B2EB"),
                    GuidNullableColumn = Guid.Parse("6D1EE7D6-F47B-45B5-AEB3-1B633AD730BD")
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
                    DateTimeColumn = new DateTime(2021, 4, 5, 15, 15, 15),
                    DateTimeNullableColumn = new DateTime(2021, 1, 2, 11, 11, 11),
                    EnumColumn = TestEnum.Second,
                    EnumNullableColumn = TestEnum.Tenth,
                    GuidColumn = Guid.Parse("A4E69C0C-75EB-47E9-BC0D-21308D91B2EB"),
                    GuidNullableColumn = Guid.Parse("4C6528BB-0270-4A08-959F-7181C5A58E21")
                },
                new TestItem
                {
                    IntColumn = 20,
                    IntNullableColumn = null,
                    DecimalColumn = 20.99m,
                    DecimalNullableColumn = 20.98m,
                    StringColumn = null,
                    BoolColumn = true,
                    BoolNullableColumn = null,
                    DateTimeColumn = new DateTime(2021, 4, 5, 15, 15, 15),
                    DateTimeNullableColumn = new DateTime(2021, 1, 2, 11, 11, 11),
                    EnumColumn = TestEnum.Second,
                    EnumNullableColumn = TestEnum.Tenth,
                    GuidColumn = Guid.Parse("AB05AED2-0E8D-405C-B4EB-5EBD6704E50D"),
                    GuidNullableColumn = null
                }
            };

            return list;
        }
    }
}