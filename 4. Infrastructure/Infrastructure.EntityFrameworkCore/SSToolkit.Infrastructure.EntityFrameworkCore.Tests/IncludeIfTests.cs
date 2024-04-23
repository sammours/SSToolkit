namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Infrastructure.EntityFrameworkCore.Extensions;
    using Xunit;

    public class IncludeIfTests
    {
        [Fact]
        public void IncludeIf_Tests()
        {
            using (var context = new StubDbContext())
            {
                // Null
                var result = context.Entities.Select(x => x).IncludeIf(null).ToList();
                result.ShouldNotBeNull();

                // one include expression
                var options = new FindOptions<Stub>
                {
                    Include = new IncludeOption<Stub>(x => x.NestedStubs)
                };
                var result2 = context.Entities.Select(x => x).IncludeIf(options).ToList();
                result2.ShouldNotBeNull();
                result2.FirstOrDefault().NestedStubs.ShouldNotBeNull();
                result2.FirstOrDefault().NestedStubs.Count.ShouldBe(1);
                result2.FirstOrDefault().NestedStubs.First().Value.ShouldBe("NestedStub 1");

                // list of includes
                options = new FindOptions<Stub>
                {
                    Includes = new List<IncludeOption<Stub>>
                    {
                        new IncludeOption<Stub>(x => x.NestedStubs),
                        new IncludeOption<Stub>(x => x.NestedStubs2)
                    }
                };
                var result3 = context.Entities.Select(x => x).IncludeIf(options).ToList();
                result3.ShouldNotBeNull();
                result3.FirstOrDefault().NestedStubs.ShouldNotBeNull();
                result3.FirstOrDefault().NestedStubs.Count.ShouldBe(1);
                result3.FirstOrDefault().NestedStubs.First().Value.ShouldBe("NestedStub 1");

                result3.FirstOrDefault().NestedStubs2.ShouldNotBeNull();
                result3.FirstOrDefault().NestedStubs2.Count.ShouldBe(1);
                result3.FirstOrDefault().NestedStubs2.First().Value.ShouldBe("NestedStub2 1");

                // Include string (nested include)
                options = new FindOptions<Stub>
                {
                    Includes = new List<IncludeOption<Stub>>
                    {
                        new IncludeOption<Stub>("NestedStubs"),
                        new IncludeOption<Stub>("NestedStubs.SecondLevelNestedStubs")
                    }
                };

                var result4 = context.Entities.Select(x => x).IncludeIf(options).ToList();
                result4.ShouldNotBeNull();
                result4.FirstOrDefault().NestedStubs.ShouldNotBeNull();
                result4.FirstOrDefault().NestedStubs.Count.ShouldBe(1);
                result4.FirstOrDefault().NestedStubs.First().Value.ShouldBe("NestedStub 1");
                result4.FirstOrDefault().NestedStubs.First().SecondLevelNestedStubs.ShouldNotBeNull();
                result4.FirstOrDefault().NestedStubs.First().SecondLevelNestedStubs.First().Value.ShouldBe("SecondLevelNestedStub 1");

                // List of include strings (nested include)
                options = new FindOptions<Stub>
                {
                    Includes = new List<IncludeOption<Stub>>
                    {
                        new IncludeOption<Stub>("NestedStubs.SecondLevelNestedStubs"),
                        new IncludeOption<Stub>("NestedStubs2.SecondLevelNestedStubs")
                    }
                };

                var result5 = context.Entities.Select(x => x).IncludeIf(options).ToList();
                result5.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs.Count.ShouldBe(1);
                result5.FirstOrDefault().NestedStubs.First().Value.ShouldBe("NestedStub 1");
                result5.FirstOrDefault().NestedStubs.First().SecondLevelNestedStubs.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs.First().SecondLevelNestedStubs.First().Value.ShouldBe("SecondLevelNestedStub 1");

                result5.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs2.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs2.Count.ShouldBe(1);
                result5.FirstOrDefault().NestedStubs2.First().Value.ShouldBe("NestedStub2 1");
                result5.FirstOrDefault().NestedStubs2.First().SecondLevelNestedStubs.ShouldNotBeNull();
                result5.FirstOrDefault().NestedStubs2.First().SecondLevelNestedStubs.First().Value.ShouldBe("SecondLevelNestedStub2 1");
            }
        }
    }
}