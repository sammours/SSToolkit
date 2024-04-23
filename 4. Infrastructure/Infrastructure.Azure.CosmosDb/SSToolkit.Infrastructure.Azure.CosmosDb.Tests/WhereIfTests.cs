namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Shouldly;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Extensions;
    using Xunit;

    public class WhereIfTests
    {
        [Fact]
        public void WhereIf_Test()
        {
            var list = new List<string> { "Item 1", "Item 2", "Item 3" };
            var result = list.AsQueryable().WhereIf(x => x == "Item 1" || x == "Item 2");
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result.First().ShouldBe("Item 1");
            result.Last().ShouldBe("Item 2");
        }
    }
}