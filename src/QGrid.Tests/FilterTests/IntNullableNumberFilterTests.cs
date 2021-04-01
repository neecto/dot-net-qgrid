using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class IntNullableNumberFilterTests : BaseFilterTests
    {
        public IntNullableNumberFilterTests(DatabaseFixture fixture) : base(fixture, "IntNullableColumn")
        {
        }

        [Fact]
        public void Int_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 5);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(5, x.IntNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 100);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, 8);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(8, x.IntNullableColumn));
            Assert.Contains(result, x => x.IntNullableColumn == null);
        }

        [Fact]
        public void Int_Neq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, new List<object> { 5, 6, 8 });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Null(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Lt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 6);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntNullableColumn < 20));
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Lt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 5);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Gt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 6);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntNullableColumn > 6));
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Gt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 8);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Lte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 5);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntNullableColumn <= 5));
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Lte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 4);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Gte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 8);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntNullableColumn >= 8));
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Int_Gte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 9);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}