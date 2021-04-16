using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class EnumNullableFilterTests : BaseFilterTests
    {
        public EnumNullableFilterTests(DatabaseFixture fixture) : base(fixture, "EnumNullableColumn")
        {
        }

        [Fact]
        public void NullableEnum_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, TestEnum.Nineth);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(TestEnum.Nineth, x.EnumNullableColumn));
        }

        [Fact]
        public void NullableEnum_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, TestEnum.First);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void NullableEnum_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, TestEnum.Nineth);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Nineth, x.EnumNullableColumn));
            Assert.Contains(result, x => x.EnumNullableColumn == null);
        }

        [Fact]
        public void NullableEnum_Neq_OnlyNulls()
        {
            var values = new List<object>
            {
                TestEnum.Nineth,
                TestEnum.Tenth
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, values);

            var result = ExecuteQuery(filters);

            Assert.All(result, x => Assert.Null(x.EnumNullableColumn));
        }

        [Fact]
        public void NullableEnum_Oneof_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, new[] { TestEnum.First, TestEnum.Nineth });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumNullableColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Third, x.EnumNullableColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Tenth, x.EnumNullableColumn));
        }

        [Fact]
        public void NullableEnum_Oneof_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, new[] { TestEnum.First, TestEnum.Second });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void NullableEnum_NotOneof_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Notoneof, new[] { TestEnum.First, TestEnum.Nineth });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.First, x.EnumNullableColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Nineth, x.EnumNullableColumn));
        }

        [Fact]
        public void NullableEnum_NotOneof_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Notoneof, new[] { TestEnum.Nineth, TestEnum.Tenth });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}