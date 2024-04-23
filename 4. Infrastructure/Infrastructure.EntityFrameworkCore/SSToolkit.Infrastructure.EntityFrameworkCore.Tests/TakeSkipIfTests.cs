namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System.Linq;
    using Shouldly;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Infrastructure.EntityFrameworkCore.Extensions;
    using Xunit;

    public class TakeSkipIfTests
    {
        [Fact]
        public void TakeSkipIf_Tests()
        {
            using (var context = new StubDbContext())
            {
                // Null
                var result = context.Entities.Select(x => x).TakeIf(null).SkipIf(null).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(10);

                // Take
                result = context.Entities.Select(x => x).TakeIf(new FindOptions<Stub>() { Take = 5 }.Take).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(5);
                result.FirstOrDefault().FirstName.ShouldBe("FirstName 1");
                result.LastOrDefault().FirstName.ShouldBe("FirstName 5");

                // Skip
                result = context.Entities.Select(x => x).SkipIf(new FindOptions<Stub>() { Skip = 1 }.Skip).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(9);
                result.FirstOrDefault().FirstName.ShouldBe("FirstName 2");
                result.LastOrDefault().FirstName.ShouldBe("FirstName 10");

                // Take, Skip
                var options = new FindOptions<Stub>() { Take = 5, Skip = 5 };
                result = context.Entities.Select(x => x).SkipIf(options.Skip).TakeIf(options.Take).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(5);
                result.FirstOrDefault().FirstName.ShouldBe("FirstName 6");
                result.LastOrDefault().FirstName.ShouldBe("FirstName 10");
            }
        }
    }
}