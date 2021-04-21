using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Update.Internal;
using QGrid.Enums;
using QGrid.Extensions;
using QGrid.Models;
using QGrid.Tests.Fixtures;
using QGrid.Tests.Models;
using Xunit;

namespace QGrid.Tests.OrderingTests
{
    [Collection("Database collection")]
    public class OrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public OrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void NoOrdering_NullOrderingList()
        {
            var result = _fixture.TestQueryable
                .ApplyOrdering(null)
                .ToList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void NoOrdering_EmptyOrderingList()
        {
            var result = _fixture.TestQueryable
                .ApplyOrdering(new List<QGridOrder>())
                .ToList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void EmptyOrderingColumn_ArgumentNullException()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("")
            };

            Assert.Throws<ArgumentNullException>(() => {
                _fixture.TestQueryable
                    .ApplyOrdering(ordering)
                    .ToList();
            });
        }

        [Fact]
        public void InvalidOrderingColumn_ArgumentException()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("InvalidColumn")
            };

            Assert.Throws<ArgumentException>(() => {
                _fixture.TestQueryable
                    .ApplyOrdering(ordering)
                    .ToList();
            });
        }

        [Fact]
        public void MultipleOrderings_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("IntColumn", OrderTypeEnum.Asc),
                new QGridOrder("BoolNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

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

                Assert.True(
                    currentItem.IntColumn <= nextItem.IntColumn
                );

                if (currentItem.IntColumn == nextItem.IntColumn)
                {
                    Assert.True(
                        currentItem.BoolNullableColumn == null ||
                        nextItem.BoolNullableColumn == null ||
                        currentItem.BoolNullableColumn == false && nextItem.BoolNullableColumn == false ||
                        currentItem.BoolNullableColumn == false && nextItem.BoolNullableColumn == true
                    );
                }
            }
        }

        [Fact]
        public void MultipleOrderings_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolColumn", OrderTypeEnum.Desc),
                new QGridOrder("EnumColumn", OrderTypeEnum.Desc),
                new QGridOrder("EnumNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

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

                Assert.True(
                    currentItem.BoolColumn == true && nextItem.BoolColumn == true ||
                    currentItem.BoolColumn == true && nextItem.BoolColumn == false ||
                    currentItem.BoolColumn == false && nextItem.BoolColumn == false
                );

                if (currentItem.BoolColumn == nextItem.BoolColumn)
                {
                    Assert.True(
                        currentItem.EnumColumn >= nextItem.EnumColumn
                    );

                    if (currentItem.EnumColumn == nextItem.EnumColumn)
                    {
                        Assert.True(
                            currentItem.EnumNullableColumn >= nextItem.EnumNullableColumn ||
                            currentItem.EnumNullableColumn == null ||
                            nextItem.EnumNullableColumn == null
                        );
                    }
                }
            }
        }

        [Fact]
        public void MultipleOrderings_DifferentTypes()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("BoolColumn", OrderTypeEnum.Asc),
                new QGridOrder("IntColumn", OrderTypeEnum.Desc),
                new QGridOrder("DateTimeColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

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

                Assert.True(
                    currentItem.BoolColumn == false && nextItem.BoolColumn == false ||
                    currentItem.BoolColumn == false && nextItem.BoolColumn == true ||
                    currentItem.BoolColumn == true && nextItem.BoolColumn == true
                );

                if (currentItem.BoolColumn == nextItem.BoolColumn)
                {
                    Assert.True(
                        currentItem.IntColumn >= nextItem.IntColumn
                    );

                    if (currentItem.IntColumn == nextItem.IntColumn)
                    {
                        Assert.True(
                            currentItem.DateTimeColumn <= nextItem.DateTimeColumn
                        );
                    }
                }
            }
        }
    }
}