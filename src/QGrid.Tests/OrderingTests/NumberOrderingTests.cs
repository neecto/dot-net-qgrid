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
    public class NumberOrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public NumberOrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Int_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("IntColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingInt(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void Int_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("IntColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingInt(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void IntNullable_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("IntNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingIntNullable(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void IntNullable_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("IntNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingIntNullable(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void Decimal_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DecimalColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDecimal(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void Decimal_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DecimalColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDecimal(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void DecimalNullable_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DecimalNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDecimalNullable(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void DecimalNullable_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DecimalNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDecimalNullable(result, OrderTypeEnum.Desc);
        }

        private void AssertOrderingInt(List<TestItem> result, OrderTypeEnum orderType)
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
                    Assert.True(currentItem.IntColumn <= nextItem.IntColumn);
                }
                else
                {
                    Assert.True(currentItem.IntColumn >= nextItem.IntColumn);
                }
            }
        }

        private void AssertOrderingIntNullable(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.IntNullableColumn == null ||
                        nextItem.IntNullableColumn == null ||
                        currentItem.IntNullableColumn <= nextItem.IntNullableColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.IntNullableColumn == null ||
                        nextItem.IntNullableColumn == null ||
                        currentItem.IntNullableColumn >= nextItem.IntNullableColumn
                    );
                }
            }
        }

        private void AssertOrderingDecimal(List<TestItem> result, OrderTypeEnum orderType)
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
                    Assert.True(currentItem.DecimalColumn <= nextItem.DecimalColumn);
                }
                else
                {
                    Assert.True(currentItem.DecimalColumn >= nextItem.DecimalColumn);
                }
            }
        }

        private void AssertOrderingDecimalNullable(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.DecimalNullableColumn == null ||
                        nextItem.DecimalNullableColumn == null ||
                        currentItem.DecimalNullableColumn <= nextItem.DecimalNullableColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.DecimalNullableColumn == null ||
                        nextItem.DecimalNullableColumn == null ||
                        currentItem.DecimalNullableColumn >= nextItem.DecimalNullableColumn
                    );
                }
            }
        }
    }
}