using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class BoolFilterTests : BaseFilterProviderTests
    {
        public BoolFilterTests(DatabaseFixture fixture) : base(fixture, "BoolColumn")
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
            var filters = CreateQGridFilters(condition, true);

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Bool_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1234);

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Bool_Eq_True()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, true);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.BoolColumn));
        }

        [Fact]
        public void Bool_Eq_False()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, false);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.False(x.BoolColumn));
        }

        [Fact]
        public void Bool_Neq_True()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, true);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.False(x.BoolColumn));
        }

        [Fact]
        public void Bool_Neq_False()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, false);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(x.BoolColumn));
        }
    }
}