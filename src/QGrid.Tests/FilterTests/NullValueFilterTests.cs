using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class NullValueFilterTests : BaseFilterProviderTests
    {
        public NullValueFilterTests(DatabaseFixture fixture) : base(fixture, "IntNullableColumn")
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
            var filters = new List<QGridFilter>
            {
                new QGridFilter("IntNullableColumn", condition, null)
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));
            });
        }

        [Fact]
        public void Null_StringProperty_Eq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("StringColumn", FilterConditionEnum.Eq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Null(x.StringColumn));
        }

        [Fact]
        public void Null_StringProperty_Neq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("StringColumn", FilterConditionEnum.Neq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotNull(x.StringColumn));
        }

        [Fact]
        public void Null_NullableProperty_Eq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("IntNullableColumn", FilterConditionEnum.Eq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Null(x.IntNullableColumn));
        }

        [Fact]
        public void Null_NullableProperty_Neq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("IntNullableColumn", FilterConditionEnum.Neq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotNull(x.IntNullableColumn));
        }

        [Fact]
        public void Null_NotNullableProperty_Eq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("DecimalColumn", FilterConditionEnum.Eq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));
            Assert.Empty(result);
        }

        [Fact]
        public void Null_NotNullableProperty_Neq()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter("DecimalColumn", FilterConditionEnum.Neq, null)
            };

            var result = ExecuteQuery(new QGridFilters(FilterOperatorEnum.And, filters));
            Assert.NotEmpty(result);
            Assert.True(result.Count == Fixture.TotalItems);
        }
    }
}