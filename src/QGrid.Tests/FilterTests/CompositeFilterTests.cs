using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class CompositeFilterTests : BaseFilterTests
    {
        public CompositeFilterTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void AndOperator_SameCondition_SameColumn()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.And,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Gt, 1),
                    new QGridFilter("IntColumn", FilterConditionEnum.Gt, 2),
                    new QGridFilter("IntColumn", FilterConditionEnum.Gt, 11)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.True(x.IntColumn > 11));
        }

        [Fact]
        public void AndOperator_SameCondition_DifferentColumns()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.And,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Eq, 20),
                    new QGridFilter("IntNullableColumn", FilterConditionEnum.Eq, null),
                    new QGridFilter("BoolColumn", FilterConditionEnum.Eq, true)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Equal(20, x.IntColumn));
            Assert.All(result, x => Assert.Null(x.IntNullableColumn));
            Assert.All(result, x => Assert.True(x.BoolColumn));
        }

        [Fact]
        public void AndOperator_DifferentConditions_SameColumn()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.And,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Gt, 1),
                    new QGridFilter("IntColumn", FilterConditionEnum.Neq, 10),
                    new QGridFilter("IntColumn", FilterConditionEnum.Lt, 20)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.True(x.IntColumn > 1));
            Assert.All(result, x => Assert.NotEqual(10, x.IntColumn));
            Assert.All(result, x => Assert.True(x.IntColumn < 20));
        }

        [Fact]
        public void AndOperator_DifferentConditions_DifferentColumns()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.And,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Gt, 1),
                    new QGridFilter("DecimalNullableColumn", FilterConditionEnum.Neq, null),
                    new QGridFilter("DecimalColumn", FilterConditionEnum.Lte, 20.5m)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.True(x.IntColumn > 1));
            Assert.All(result, x => Assert.NotNull(x.DecimalNullableColumn));
            Assert.All(result, x => Assert.True(x.DecimalColumn <= 20.5m));
        }

        [Fact]
        public void AndOperator_MutuallyExclusiveConditions_NoResults()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.And,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Eq, 20),
                    new QGridFilter("IntColumn", FilterConditionEnum.Neq, 20)
                }
            };

            var result = ExecuteQuery(filters);
            Assert.Empty(result);
        }

        [Fact]
        public void OrOperator_SameCondition_SameColumn()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.Or,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("EnumColumn", FilterConditionEnum.Eq, 1),
                    new QGridFilter("EnumColumn", FilterConditionEnum.Eq, 3)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Nineth, x.EnumColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Tenth, x.EnumColumn));
        }

        [Fact]
        public void OrOperator_SameCondition_DifferentColumns()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.Or,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("StringColumn", FilterConditionEnum.Eq, "case invariant?"),
                    new QGridFilter("BoolNullableColumn", FilterConditionEnum.Eq, null)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(
                result,
                x => Assert.True(x.StringColumn?.ToLowerInvariant() == "case invariant?" || x.BoolNullableColumn == null)
            );
        }

        [Fact]
        public void OrOperator_DifferentConditions_SameColumn()
        {
            var testDate = new DateTime(2021, 1, 1, 13, 13, 13);

            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.Or,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("DateTimeNullableColumn", FilterConditionEnum.Eq, null),
                    new QGridFilter("DateTimeNullableColumn", FilterConditionEnum.Eqdate, testDate)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(
                result,
                x => Assert.True(x.DateTimeNullableColumn == null || x.DateTimeNullableColumn.Value.Date == testDate.Date)
            );
        }

        [Fact]
        public void OrOperator_DifferentConditions_DifferentColumns()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.Or,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("StringColumn", FilterConditionEnum.Startswith, "CASE"),
                    new QGridFilter("IntNullableColumn", FilterConditionEnum.Gte, 8),
                    new QGridFilter("DecimalColumn", FilterConditionEnum.Eq, 1.99m)
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(
                result,
                x => Assert.True(
                    x.StringColumn?.ToLowerInvariant().StartsWith("case") ?? false
                    || x.IntNullableColumn >= 8
                    || x.DecimalColumn == 1.99m
                    )
            );
        }

        [Fact]
        public void AndOperator_MutuallyExclusiveConditions_AllResults()
        {
            var filters = new QGridFilters
            {
                Operator = FilterOperatorEnum.Or,
                Filters = new List<QGridFilter>
                {
                    new QGridFilter("IntColumn", FilterConditionEnum.Eq, 5),
                    new QGridFilter("IntColumn", FilterConditionEnum.Neq, 5)
                }
            };

            var result = ExecuteQuery(filters);
            Assert.NotEmpty(result);
            Assert.True(result.Count == Fixture.TotalItems);
        }
    }
}