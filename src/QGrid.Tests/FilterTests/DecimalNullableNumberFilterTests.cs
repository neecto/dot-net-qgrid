using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class DecimalNullableNumberFilterTests : BaseFilterTests
    {
        public DecimalNullableNumberFilterTests(DatabaseFixture fixture) : base(fixture, "DecimalNullableColumn")
        {
        }

        [Fact]
        public void DecimalNullable_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 9.99m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(9.99m, x.DecimalNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 9.98m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DecimalNullable_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, 20.98m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(20.98m, x.DecimalNullableColumn));
            Assert.Contains(result, x => x.DecimalNullableColumn == null);
        }

        [Fact]
        public void DecimalNullable_Neq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, new List<object> { 9.99m, 21.55m, 20.98m });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Null(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Lt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 20.98m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalNullableColumn < 20.98m));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Lt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 9.99m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DecimalNullable_Gt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 20.90m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalNullableColumn > 20.90m));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Gt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 21.55m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DecimalNullable_Lte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 9.99m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalNullableColumn <= 9.99m));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Lte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 9.97m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DecimalNullable_Gte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 21.55m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalNullableColumn >= 21.55m));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
        }

        [Fact]
        public void DecimalNullable_Gte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 21.56m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}