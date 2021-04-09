using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class BoolNullableFilterTests : BaseFilterTests
    {
        public BoolNullableFilterTests(DatabaseFixture fixture) : base(fixture, "BoolNullableColumn")
        {
        }

        [Fact]
        public void BoolNullable_Eq_True()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, true);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.BoolNullableColumn));
        }

        [Fact]
        public void BoolNullable_Eq_False()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, false);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.False(x.BoolNullableColumn));
        }

        [Fact]
        public void BoolNullable_Neq_True()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, true);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.DoesNotContain(result, x => x.BoolNullableColumn == true);
            Assert.Contains(result, x => x.BoolNullableColumn == null);
        }

        [Fact]
        public void BoolNullable_Neq_False()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, false);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.DoesNotContain(result, x => x.BoolNullableColumn == false);
            Assert.Contains(result, x => x.BoolNullableColumn == null);
        }
    }
}