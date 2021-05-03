using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class GuildNullableFilterTests : BaseFilterProviderTests
    {
        public GuildNullableFilterTests(DatabaseFixture fixture) : base(fixture, "GuidNullableColumn")
        {
        }

        [Fact]
        public void GuidNullable_Eq_HasResults_WithDashes()
        {
            var testValue = Guid.Parse("6D1EE7D6-F47B-45B5-AEB3-1B633AD730BD");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testValue, x.GuidNullableColumn));
        }

        [Fact]
        public void GuidNullable_Eq_HasResults_NoDashes()
        {
            var testValue = Guid.Parse("6D1EE7D6F47B45B5AEB31B633AD730BD");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testValue, x.GuidNullableColumn));
        }

        [Fact]
        public void GuidNullable_Eq_NoResults()
        {
            var testValue = Guid.Parse("C670D185-7C08-46DB-BE24-03A6A59F6174");
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testValue);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void GuidNullable_Neq_HasResults()
        {
            var testValue = Guid.Parse("6D1EE7D6-F47B-45B5-AEB3-1B633AD730BD");
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testValue);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(testValue, x.GuidNullableColumn));
            Assert.Contains(result, x => x.GuidNullableColumn == null);
        }

        [Fact]
        public void GuidNullable_Neq_OnlyNulls()
        {
            var testValues = new List<object>
            {
                Guid.Parse("6D1EE7D6-F47B-45B5-AEB3-1B633AD730BD"),
                Guid.Parse("4C6528BB-0270-4A08-959F-7181C5A58E21")
            };

            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testValues);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Null(x.GuidNullableColumn));
        }
    }
}