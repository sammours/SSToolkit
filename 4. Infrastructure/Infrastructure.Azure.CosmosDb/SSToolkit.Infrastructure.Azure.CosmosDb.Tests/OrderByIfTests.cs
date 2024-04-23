namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Shouldly;
    using SSToolkit.Infrastructure.Azure.CosmosDb.Extensions;
    using Xunit;

    public class OrderByIfTests
    {
        [Fact]
        public void OrderByIf_Test()
        {
            var list = new List<string> { "Item 1", "Item 2" };
            var result = list.AsQueryable().OrderByIf(null as Expression<Func<string, object>>);
            result.ShouldNotBeNull();
            result.First().ShouldBe("Item 1");
            result.Last().ShouldBe("Item 2");

            result = list.AsQueryable().OrderByIf(x => x);
            result.ShouldNotBeNull();
            result.First().ShouldBe("Item 1");
            result.Last().ShouldBe("Item 2");

            result = list.AsQueryable().OrderByIf(x => x, true);
            result.ShouldNotBeNull();
            result.First().ShouldBe("Item 2");
            result.Last().ShouldBe("Item 1");
        }
    }
}