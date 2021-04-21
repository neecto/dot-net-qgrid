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
    public class DateTimeOrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public DateTimeOrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DateTime_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DateTimeColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDateTime(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void DateTime_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DateTimeColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDateTime(result, OrderTypeEnum.Desc);
        }

        [Fact]
        public void DateTimeNullable_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DateTimeNullableColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDateTimeNullable(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void DateTimeNullable_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("DateTimeNullableColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingDateTimeNullable(result, OrderTypeEnum.Desc);
        }

        private void AssertOrderingDateTime(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.DateTimeColumn <= nextItem.DateTimeColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.DateTimeColumn >= nextItem.DateTimeColumn
                    );
                }
            }
        }

        private void AssertOrderingDateTimeNullable(List<TestItem> result, OrderTypeEnum orderType)
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
                        currentItem.DateTimeNullableColumn == null ||
                        nextItem.DateTimeNullableColumn == null ||
                        currentItem.DateTimeNullableColumn <= nextItem.DateTimeNullableColumn
                    );
                }
                else
                {
                    Assert.True(
                        currentItem.DateTimeNullableColumn == null ||
                        nextItem.DateTimeNullableColumn == null ||
                        currentItem.DateTimeNullableColumn >= nextItem.DateTimeNullableColumn
                    );
                }
            }
        }
    }
}