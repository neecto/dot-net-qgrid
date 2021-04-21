using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.OrderingTests
{
    [Collection("Database collection")]
    public class EnumOrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public EnumOrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Enum_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("EnumColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingEnum(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void Enum_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("EnumColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingEnum(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void EnumNullable_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("EnumNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingEnumNullable(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void EnumNullable_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("EnumNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingEnumNullable(result, OrderTypeEnum.Desc);
        }

        private void AssertOrderingEnum(List<TestItem> result, OrderTypeEnum orderType)
        {
            for (int i = 0; i < result.Count; i++)
            {
                var currentItem = result[i];
                var nextItem = i + 1 == result.Count
                    ? null
                    : result[i + 1];

                if (nextItem == null)
                {
                    break;
                }

                if (orderType == OrderTypeEnum.Asc)
                {
                    Assert.True(
                        currentItem.EnumColumn <= nextItem.EnumColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.EnumColumn >= nextItem.EnumColumn
                    );
                }
            }
        }

        private void AssertOrderingEnumNullable(List<TestItem> result, OrderTypeEnum orderType)
        {
            for (int i = 0; i < result.Count; i++)
            {
                var currentItem = result[i];
                var nextItem = i + 1 == result.Count
                    ? null
                    : result[i + 1];

                if (nextItem == null)
                {
                    break;
                }

                if (orderType == OrderTypeEnum.Asc)
                {
                    Assert.True(
                        currentItem.EnumNullableColumn == null ||
                        nextItem.EnumNullableColumn == null ||
                        currentItem.EnumNullableColumn <= nextItem.EnumNullableColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.EnumNullableColumn == null ||
                        nextItem.EnumNullableColumn == null ||
                        currentItem.EnumNullableColumn >= nextItem.EnumNullableColumn
                    );
                }
            }
        }
    }
}