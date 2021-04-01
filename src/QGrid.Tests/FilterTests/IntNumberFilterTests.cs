using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class IntNumberFilterTests : BaseFilterTests
    {
        public IntNumberFilterTests(DatabaseFixture fixture) : base(fixture, "IntColumn")
        {
        }

        public static IEnumerable<object[]> NotSupportedConditions =>
            new List<object[]>
            {
                new object[] { FilterConditionEnum.Contains },
                new object[] { FilterConditionEnum.Startswith },
                new object[] { FilterConditionEnum.Endswith },
                new object[] { FilterConditionEnum.Doesnotcontain },
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
            var filters = CreateQGridFilters(condition, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Int_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1.05m);

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Int_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(1, x.IntColumn));
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
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, 20);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(20, x.IntColumn));
        }

        [Fact]
        public void Int_Neq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, new List<object> {1, 2, 10, 20});

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Lt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 20);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntColumn < 20));
        }

        [Fact]
        public void Int_Lt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, 1);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Gt_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 10);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntColumn > 10));
        }

        [Fact]
        public void Int_Gt_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, 20);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Lte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 2);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntColumn <= 2));
        }

        [Fact]
        public void Int_Lte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, 0);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Gte_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 20);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.IntColumn >= 20));
        }

        [Fact]
        public void Int_Gte_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, 21);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}