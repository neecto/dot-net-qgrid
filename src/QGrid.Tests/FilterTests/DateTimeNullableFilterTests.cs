using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class DateTimeNullableFilterTests : BaseFilterTests
    {
        public DateTimeNullableFilterTests(DatabaseFixture fixture) : base(fixture, "DateTimeNullableColumn")
        {
        }

        [Fact]
        public void DateTimeNullable_Eq_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.Equal(testDate, x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Eq_NoResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 10);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Eqdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Eqdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.Equal(testDate.Date, x.DateTimeNullableColumn.Value.Date));
        }

        [Fact]
        public void DateTimeNullable_Eqdate_NoResults()
        {
            var testDate = new DateTime(2021, 2, 5, 12, 12, 55);
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Neq_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 13, 13, 13);
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(testDate, x.DateTimeNullableColumn));
            Assert.Contains(result, x => x.DateTimeNullableColumn == null);
        }

        [Fact]
        public void DateTimeNullable_Neq_OnlyNulls()
        {
            var testDates = new List<object>
            {
                new DateTime(2021, 1, 1, 12, 12, 12),
                new DateTime(2021, 1, 1, 13, 13, 13),
                new DateTime(2021, 1, 2, 11, 11, 11)
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, testDates);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Null(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Neqdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Neqdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(
                result,
                x => Assert.True(x.DateTimeNullableColumn == null ||
                                 x.DateTimeNullableColumn.Value.Date != testDate.Date)
            );
        }

        [Fact]
        public void DateTimeNullable_Neqdate_OnlyNulls()
        {
            var testDates = new List<object>
            {
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 2, 0, 0, 0)
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neqdate, testDates);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Null(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Lt_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 13, 13, 13);
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate > x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Lt_NoResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Lt, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Ltdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 2, 14, 14, 14);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date > x.DateTimeNullableColumn.Value.Date));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Ltdate_NoResults()
        {
            var testDate = new DateTime(2021, 1, 1, 14, 14, 14);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Gt_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate < x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Gt_NoResults()
        {
            var testDate = new DateTime(2021, 1, 2, 11, 11, 11);
            var filters = CreateQGridFilters(FilterConditionEnum.Gt, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Gtdate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date < x.DateTimeNullableColumn.Value.Date));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Gtdate_NoResults()
        {
            var testDate = new DateTime(2021, 1, 2, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtdate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Lte_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate >= x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Lte_NoResults()
        {
            var testDate = new DateTime(2021, 1, 1, 12, 12, 11);
            var filters = CreateQGridFilters(FilterConditionEnum.Lte, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Ltedate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 1, 0, 0, 0);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date >= x.DateTimeNullableColumn.Value.Date));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Ltedate_NoResults()
        {
            var testDate = new DateTime(2020, 12, 31, 23, 59, 59);
            var filters = CreateQGridFilters(FilterConditionEnum.Ltedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Gte_HasResults()
        {
            var testDate = new DateTime(2021, 1, 2, 11, 11, 11);
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate <= x.DateTimeNullableColumn));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Gte_NoResults()
        {
            var testDate = new DateTime(2021, 1, 2, 11, 11, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Gte, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void DateTimeNullable_Gtedate_HasResults()
        {
            var testDate = new DateTime(2021, 1, 2, 11, 11, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.True(testDate.Date <= x.DateTimeNullableColumn.Value.Date));
            Assert.All(result, x => Assert.NotNull(x.DateTimeNullableColumn));
        }

        [Fact]
        public void DateTimeNullable_Gtedate_NoResults()
        {
            var testDate = new DateTime(2021, 1, 3, 11, 11, 12);
            var filters = CreateQGridFilters(FilterConditionEnum.Gtedate, testDate);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}