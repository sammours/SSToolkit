namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Extensions;
    using Xunit;

    public class SkipIfTests
    {
        [Fact]
        public void SkipIf_Test()
        {
            var list = new List<string> { "Item 1", "Item 2", "Item 3" };
            var result = list.AsQueryable().SkipIf(null);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(list.Count);

            result = list.AsQueryable().SkipIf(1);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result.First().ShouldBe("Item 2");
            result.Last().ShouldBe("Item 3");
        }
    }
}