using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class GuidFilterTests : BaseFilterProviderTests
    {
        public GuidFilterTests(DatabaseFixture fixture) : base(fixture, "GuidColumn")
        {
        }

        public static IEnumerable<object[]> NotSupportedConditions =>
            new List<object[]>
            {
                new object[] { FilterConditionEnum.Contains },
                new object[] { FilterConditionEnum.Startswith },
                new object[] { FilterConditionEnum.Endswith },
                new object[] { FilterConditionEnum.Doesnotcontain },
                new object[] { FilterConditionEnum.Lt },
                new object[] { FilterConditionEnum.Gt },
                new object[] { FilterConditionEnum.Lte },
                new object[] { FilterConditionEnum.Gte },
                new object[] { FilterConditionEnum.Eqdate },
                new object[] { FilterConditionEnum.Neqdate },
                new object[] { FilterConditionEnum.Ltdate },
                new object[] { FilterConditionEnum.Gtdate },
                new object[] { FilterConditionEnum.Ltedate },
                new object[] { FilterConditionEnum.Gtedate },
                new object[] { FilterConditionEnum.Oneof },
                new object[] { FilterConditionEnum.Notoneof }
            };

        [Theory]
        [MemberData(nameof(NotSupportedConditions))]
        public void NotSupportedConditions_ShouldThrowArgumentOutOfRangeException_Theory(FilterConditionEnum condition)
        {
            var filters = CreateQGridFilters(condition, "23C924B6-79CC-4F3C-9B41-5A71E03AF3D7");

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Guid_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "not-a-guid");

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Guid_Eq_HasResults_WithDashes()
        {
            var testValue = Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testValue, x.GuidColumn));
        }

        [Fact]
        public void Guid_Eq_HasResults_NoDashes()
        {
            var testValue = Guid.Parse("394BBB1303CC4C0181DEDBE78EDFF011");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testValue, x.GuidColumn));
        }

        [Fact]
        public void Guid_Eq_NoResults()
        {
            var testValue = Guid.Parse("C670D185-7C08-46DB-BE24-03A6A59F6174");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Guid_Neq_HasResults()
        {
            var testValue = Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011");
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(testValue, x.GuidColumn));
        }

        [Fact]
        public void Guid_Neq_NoResults()
        {
            var testValues = new List<object>
            {
                Guid.Parse("394BBB13-03CC-4C01-81DE-DBE78EDFF011"),
                Guid.Parse("A4E69C0C-75EB-47E9-BC0D-21308D91B2EB"),
                Guid.Parse("AB05AED2-0E8D-405C-B4EB-5EBD6704E50D")
            };

            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testValues);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}