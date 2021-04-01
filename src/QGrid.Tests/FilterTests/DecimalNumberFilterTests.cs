using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class DecimalNumberFilterTests : BaseFilterTests
    {
        public DecimalNumberFilterTests(DatabaseFixture fixture) : base(fixture, "DecimalColumn")
        {
        }

        [Fact]
        public void Decimal_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "123;123");

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Decimal_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1.99m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(1.99m, x.DecimalColumn));
        }

        [Fact]
        public void Decimal_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1.98m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Decimal_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, 20.50);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(20.50m, x.DecimalColumn));
        }

        [Fact]
        public void Decimal_Neq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, new List<object> { 1.99m, 1.85m, 1.51m, 20.50m, 20.99m });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Decimal_Lt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 20.50m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalColumn < 20.50m));
        }

        [Fact]
        public void Decimal_Lt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 1.99m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Decimal_Gt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 1.5m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalColumn > 1.5m));
        }

        [Fact]
        public void Decimal_Gt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 20.99m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Decimal_Lte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 1.99m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalColumn <= 1.99m));
        }

        [Fact]
        public void Decimal_Lte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 1.98m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Decimal_Gte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 20.99m);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.DecimalColumn >= 20.99m));
        }

        [Fact]
        public void Decimal_Gte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 21.01m);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}