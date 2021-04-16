using QGrid.Tests.Fixtures;
using Xunit;

namespace QGrid.Tests.FilterTests
{
    [Collection("Database collection")]
    public class CompositeFilterTests : BaseFilterTests
    {
        public CompositeFilterTests(DatabaseFixture fixture) : base(fixture)
        {
        }
    }
}