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
    public class BoolOrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public BoolOrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Bool_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingBool(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void Bool_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingBool(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void BoolNullable_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingBoolNullable(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void BoolNullable_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingBoolNullable(result, OrderTypeEnum.Desc);
        }

        private void AssertOrderingBool(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.BoolColumn == false && nextItem.BoolColumn == false ||
                        currentItem.BoolColumn == false && nextItem.BoolColumn == true ||
                        currentItem.BoolColumn == true && nextItem.BoolColumn == true
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.BoolColumn == true && nextItem.BoolColumn == true ||
                        currentItem.BoolColumn == true && nextItem.BoolColumn == false ||
                        currentItem.BoolColumn == false && nextItem.BoolColumn == false
                    );
                }
            }
        }

        private void AssertOrderingBoolNullable(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.BoolNullableColumn == null ||
                        nextItem.BoolNullableColumn == null ||
                        currentItem.BoolNullableColumn == false && nextItem.BoolNullableColumn == false ||
                        currentItem.BoolNullableColumn == false && nextItem.BoolNullableColumn == true ||
                        currentItem.BoolNullableColumn == true && nextItem.BoolNullableColumn == true
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.BoolNullableColumn == null ||
                        nextItem.BoolNullableColumn == null ||
                        currentItem.BoolNullableColumn == true && nextItem.BoolNullableColumn == true ||
                        currentItem.BoolNullableColumn == true && nextItem.BoolNullableColumn == false ||
                        currentItem.BoolNullableColumn == false && nextItem.BoolNullableColumn == false
                    );
                }
            }
        }
    }
}