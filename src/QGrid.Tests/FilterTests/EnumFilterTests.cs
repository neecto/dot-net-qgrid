using System;
using System.Collections.Generic;
using QGrid.Enums;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class EnumFilterTests : BaseFilterProviderTests
    {
        public EnumFilterTests(DatabaseFixture fixture) : base(fixture, "EnumColumn")
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
                new object[] { FilterConditionEnum.Gtedate }
            };

        public static IEnumerable<object[]> InvalidValues =>
            new List<object[]>
            {
                new object[] {123},
                new object[] {"test"}
            };

        [Theory]
        [MemberData(nameof(NotSupportedConditions))]
        public void NotSupportedConditions_ShouldThrowArgumentOutOfRangeException_Theory(FilterConditionEnum condition)
        {
            var filters = CreateQGridFilters(condition, TestEnum.First);

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Theory]
        [MemberData(nameof(InvalidValues))]
        public void Enum_InvalidValue_ShouldThrowArgumentException(object value)
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, value);

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Enum_Oneof_NotCollection_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, TestEnum.First);

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Enum_Oneof_InvalidValueInCollection_ShouldThrowArgumentException()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, new [] { "1", "test", "3"});

            Assert.Throws<ArgumentException>(() => {
                ExecuteQuery(filters);
            });
        }

        [Fact]
        public void Enum_EnumValue_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, TestEnum.First);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(TestEnum.First, x.EnumColumn));
        }

        [Fact]
        public void Enum_StringValue_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "first");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(TestEnum.First, x.EnumColumn));
        }

        [Fact]
        public void Enum_IntValue_Eq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 1);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.Equal(TestEnum.First, x.EnumColumn));
        }

        [Fact]
        public void Enum_EnumValue_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, TestEnum.Tenth);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Enum_StringValue_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, "tenth");

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Enum_IntValue_Eq_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Eq, 10);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Enum_EnumValue_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, TestEnum.Second);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumColumn));
        }

        [Fact]
        public void Enum_StringValue_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, "second");

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumColumn));
        }

        [Fact]
        public void Enum_IntValue_Neq_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, 2);

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumColumn));
        }

        [Fact]
        public void Enum_Value_Neq_NoResults()
        {
            var values = new List<object>
            {
                "first",
                2,
                TestEnum.Third
            };
            var filters = CreateQGridFilters(FilterConditionEnum.Neq, values);

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Enum_OneOf_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, new[] {1, 3, 10});

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.Second, x.EnumColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Nineth, x.EnumColumn));
        }

        [Fact]
        public void Enum_OneOf_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Oneof, new[] { 9, 10 });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }

        [Fact]
        public void Enum_NotOneOf_HasResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Notoneof, new[] { 1, 3, 10 });

            var result = ExecuteQuery(filters);

            Assert.NotEmpty(result);
            Assert.True(result.Count < Fixture.TotalItems);
            Assert.All(result, x => Assert.NotEqual(TestEnum.First, x.EnumColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Third, x.EnumColumn));
            Assert.All(result, x => Assert.NotEqual(TestEnum.Tenth, x.EnumColumn));
        }

        [Fact]
        public void Enum_NotOneOf_NoResults()
        {
            var filters = CreateQGridFilters(FilterConditionEnum.Notoneof, new[] { 1, 2, 3 });

            var result = ExecuteQuery(filters);

            Assert.Empty(result);
        }
    }
}