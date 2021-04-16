using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class StringFilterTests : BaseFilterProviderTests
    {
        public StringFilterTests(DatabaseFixture fixture) : base(fixture, "StringColumn")
        {
        }

        public static IEnumerable<object[]> NotSupportedConditions =>
            new List<object[]>
            {
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
            var filters = CreateQGridFilters(condition, "test");

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void String_InvalidValue_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, new { a = 123 });

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void String_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "This is a string");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal("This is a string", x.StringColumn));
        }

        [Fact]
        public void String_Eq_IgnoreCase_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "CASE INVARIANT?");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count > 1);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal("case invariant?".ToLowerInvariant(), x.StringColumn.ToLowerInvariant()));
        }

        [Fact]
        public void String_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "test");

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void String_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, "This is a string");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.Contains(result, x => x.IntNullableColumn == null);
            Assert.All(result, x => Assert.NotEqual("This is a string", x.StringColumn));
        }

        [Fact]
        public void String_Neq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, new List<object> { "This is a string", "case invariant?" });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Null(x.StringColumn));
        }

        [Fact]
        public void String_Contains_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Contains, "var");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.True(result.Count > 1);
            Assert.All(result, x => Assert.Contains("var", x.StringColumn));
        }

        [Fact]
        public void String_Contains_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Contains, "test");

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void String_DoesNotContain_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Doesnotcontain, "case");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.DoesNotContain("case", x.StringColumn));
        }

        [Fact]
        public void String_DoesNotContain_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Doesnotcontain, new List<object> { "This is a string", "case invariant?" });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void String_StartsWith_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Startswith, "this is");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.StartsWith("this is".ToLowerInvariant(), x.StringColumn.ToLowerInvariant()));
        }

        [Fact]
        public void String_StartsWith_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Startswith, "test");

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void String_EndsWith_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Endswith, "?");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.EndsWith("?", x.StringColumn));
        }

        [Fact]
        public void String_EndsWith_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Endswith, "test");

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}