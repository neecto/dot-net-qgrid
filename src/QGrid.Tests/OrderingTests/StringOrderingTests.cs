using System;
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
    public class StringOrderingTests
    {
        private readonly DatabaseFixture _fixture;

        public StringOrderingTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void String_Asc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("StringColumn", OrderTypeEnum.Asc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingString(result, OrderTypeEnum.Asc);
        }

        [Fact]
        public void String_Desc()
        {
            var ordering = new List<QGridOrder>
            {
                new QGridOrder("StringColumn", OrderTypeEnum.Desc)
            };

            var result = _fixture.TestQueryable
                .ApplyOrdering(ordering)
                .ToList();

            AssertOrderingString(result, OrderTypeEnum.Desc);
        }

        private void AssertOrderingString(List<TestItem> result, OrderTypeEnum orderType)
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

                int comparison = string.Compare(currentItem.StringColumn, nextItem.StringColumn, StringComparison.OrdinalIgnoreCase);
                if (orderType == OrderTypeEnum.Asc)
                {
                    Assert.True(comparison <= 0);
                }
                else
                {
                    Assert.True(comparison >= 0);
                }
            }
        }
    }
}