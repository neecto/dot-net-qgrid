using System;
using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class NumberFilterTests
    {
        private readonly DatabaseFixture _fixture;

        public NumberFilterTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
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
        public void NotSupportedConditions_Theory(FilterConditionEnum condition)
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = condition,
                    Value = 1
                }
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Int_Eq_HasResults()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Eq,
                    Value = 1
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < _fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(1, x.IntColumn));
        }

        [Fact]
        public void Int_Eq_NoResults()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Eq,
                    Value = 100
                }
            };

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Neq_HasResults()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Neq,
                    Value = 20
                }
            };

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < _fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(20, x.IntColumn));
        }

        [Fact]
        public void Int_Neq_NoResults()
        {
            var filters = new List<QGridFilter>
            {
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Neq,
                    Value = 1
                },
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Neq,
                    Value = 2
                },
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Neq,
                    Value = 10
                },
                new QGridFilter
                {
                    Column = "IntColumn",
                    Condition = FilterConditionEnum.Neq,
                    Value = 20
                }
            };

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Int_Lt_HasResults()
        {

        }

        [Fact]
        public void Int_Lt_NoResults()
        {

        }

        [Fact]
        public void Int_Gt_HasResults()
        {

        }

        [Fact]
        public void Int_Gt_NoResults()
        {

        }

        [Fact]
        public void Int_Lte_HasResults()
        {

        }

        [Fact]
        public void Int_Lte_NoResults()
        {

        }

        [Fact]
        public void Int_Gte_HasResults()
        {

        }

        [Fact]
        public void Int_Gte_NoResults()
        {

        }

        [Fact]
        public void Double_Eq_HasResults()
        {
        }

        [Fact]
        public void Double_Eq_NoResults()
        {

        }

        [Fact]
        public void Double_Neq_HasResults()
        {

        }

        [Fact]
        public void Double_Neq_NoResults()
        {

        }

        [Fact]
        public void Double_Lt_HasResults()
        {

        }

        [Fact]
        public void Double_Lt_NoResults()
        {

        }

        [Fact]
        public void Double_Gt_HasResults()
        {

        }

        [Fact]
        public void Double_Gt_NoResults()
        {

        }

        [Fact]
        public void Double_Lte_HasResults()
        {

        }

        [Fact]
        public void Double_Lte_NoResults()
        {

        }

        [Fact]
        public void Double_Gte_HasResults()
        {

        }

        [Fact]
        public void Double_Gte_NoResults()
        {

        }

        private List<TestItem> ExecuteQuery(List<QGridFilter> filters) =>
            _fixture.TestQueryable
                .ApplyFilters(filters)
                .ToList();
    }
}