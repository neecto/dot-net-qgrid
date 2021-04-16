using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class DateTimeFilterTests : BaseFilterProviderTests
    {
        public DateTimeFilterTests(DatabaseFixture fixture) : base(fixture, "DateTimeColumn")
        {
        }

        public static IEnumerable<object[]> NotSupportedConditions =>
            new List<object[]>
            {
                new object[] { FilterConditionEnum.Contains },
                new object[] { FilterConditionEnum.Startswith },
                new object[] { FilterConditionEnum.Endswith },
                new object[] { FilterConditionEnum.Doesnotcontain },
                new object[] { FilterConditionEnum.Oneof },
                new object[] { FilterConditionEnum.Notoneof }
            };

        [Theory]
        [MemberData(nameof(NotSupportedConditions))]
        public void NotSupportedConditions_ShouldThrowArgumentOutOfRangeException_Theory(FilterConditionEnum condition)
        {
            var filters = CreateQGridFilters(condition, DateTime.UtcNow);

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void DateTime_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 5);

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void DateTime_Eq_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 54);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testDate, x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Eq_NoResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 55);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Eqdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 55);
            var filters = CreateQGridFilters(FilterConditionEnum.Eqdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(testDate.Date, x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Eqdate_NoResults()
        {
            var testDate = new DateTime(2021, 2, 5, 12, 12, 55);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Neq_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 54);
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(testDate, x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Neq_NoResults()
        {
            var testDates = new List<object>
            {
                new DateTime(2021, 1, 5, 12, 12, 54),
                new DateTime(2021, 1, 5, 12, 12, 56),
                new DateTime(2021, 1, 5, 18, 0, 0),
                new DateTime(2021, 4, 5, 14, 10, 53),
                new DateTime(2021, 4, 5, 15, 15, 15)
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testDates);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Neqdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 55);
            var filters = CreateQGridFilters(FilterConditionEnum.Neqdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(testDate.Date, x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Neqdate_NoResults()
        {
            var testDates = new List<object>
            {
                new DateTime(2021, 1, 5, 0, 0, 0),
                new DateTime(2021, 4, 5, 0, 0, 0)
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neqdate, testDates);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Lt_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 56);
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate > x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Lt_NoResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 54);
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Ltdate_HasResults()
        {
            var testDate = new DateTime(2021, 4, 5, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date > x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Ltdate_NoResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 56);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Gt_HasResults()
        {
            var testDate = new DateTime(2021, 4, 5, 14, 10, 53);
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate < x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Gt_NoResults()
        {
            var testDate = new DateTime(2021, 4, 5, 15, 15, 15);
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Gtdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 14, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date < x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Gtdate_NoResults()
        {
            var testDate = new DateTime(2021, 4, 5, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Lte_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 56);
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate >= x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Lte_NoResults()
        {
            var testDate = new DateTime(2021, 1, 5, 12, 12, 53);
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Ltedate_HasResults()
        {
            var testDate = new DateTime(2021, 4, 5, 14, 10, 53);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.Equal(Fixture.TotalItems, result.Count);
            Assert.All(result, x => Assert.True(testDate.Date >= x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Ltedate_NoResults()
        {
            var testDate = new DateTime(2021, 1, 4, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Gte_HasResults()
        {
            var testDate = new DateTime(2021, 4, 5, 14, 10, 53);
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate <= x.DateTimeColumn));
        }

        [Fact]
        public void DateTime_Gte_NoResults()
        {
            var testDate = new DateTime(2021, 4, 5, 15, 15, 16);
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTime_Gtedate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 5, 18, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.Equal(Fixture.TotalItems, result.Count);
            Assert.All(result, x => Assert.True(testDate.Date <= x.DateTimeColumn.Date));
        }

        [Fact]
        public void DateTime_Gtedate_NoResults()
        {
            var testDate = new DateTime(2021, 4, 6, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}