namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Extensions;
    using Xunit;

    public class TakeIfTests
    {
        [Fact]
        public void TakeIf_Test()
        {
            var list = new List<string> { "Item 1", "Item 2", "Item 2" };
            var result = list.AsQueryable().TakeIf(null);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(list.Count);

            result = list.AsQueryable().TakeIf(2);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result.First().ShouldBe("Item 1");
            result.Last().ShouldBe("Item 2");
        }
    }
}