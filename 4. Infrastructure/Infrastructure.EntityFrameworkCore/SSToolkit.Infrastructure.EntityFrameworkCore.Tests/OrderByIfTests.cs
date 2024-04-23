namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Infrastructure.EntityFrameworkCore.Extensions;
    using Xunit;

    public class OrderByIfTests
    {
        [Fact]
        public void OrderByIf_Tests()
        {
            using (var context = new StubDbContext())
            {
                // Null
                var result = context.Entities.Select(x => x).OrderByIf(null).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(10);
                result.FirstOrDefault().FirstName.ShouldBe("FirstName 1");
                result.LastOrDefault().FirstName.ShouldBe("FirstName 10");

                // One order
                var options = new FindOptions<Stub>
                {
                    Order = new OrderByOption<Stub>(x => x.FirstName, OrderByDirection.Desc)
                };

                var result1 = context.Entities.Select(x => x).OrderByIf(options).ToList();
                result1.ShouldNotBeNull();
                result1.Count.ShouldBe(10);
                result1.FirstOrDefault().FirstName.ShouldBe("FirstName 9");
                result1.LastOrDefault().FirstName.ShouldBe("FirstName 1");

                // Multiple specifications
                options = new FindOptions<Stub>
                {
                    Orders = new List<OrderByOption<Stub>>
                    {
                        new OrderByOption<Stub>(x => x.Age, OrderByDirection.Asc),
                        new OrderByOption<Stub>(x => x.FirstName, OrderByDirection.Desc),
                    }
                };

                var result2 = context.Entities.Select(x => x).OrderByIf(options).ToList();
                result2.ShouldNotBeNull();
                result2.Count.ShouldBe(10);
                result2.FirstOrDefault().FirstName.ShouldBe("FirstName 1");
                result2.LastOrDefault().FirstName.ShouldBe("FirstName 10");
            }
        }
    }
}