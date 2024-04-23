namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Infrastructure.EntityFrameworkCore.Extensions;
    using Xunit;

    public class WhereIfTests
    {
        [Fact]
        public void WhereIf_Tests()
        {
            using (var context = new StubDbContext())
            {
                // Null
                var result = context.Entities.Select(x => x).WhereIf(expression: null).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(10);

                // One Specification
                var specification = new Specification<Stub>(x => x.FirstName == "FirstName 1" || x.FirstName == "FirstName 2");
                result = context.Entities.Select(x => x).WhereIf(specification.ToExpression()).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(2);

                // Multiple specifications
                var specifications = new List<Specification<Stub>>
                {
                    new Specification<Stub>(x => x.FirstName.StartsWith("FirstName")),
                    new Specification<Stub>(x => x.FirstName.EndsWith("1"))
                };

                result = context.Entities.Select(x => x).WhereIf(specifications.Select(x => x.ToExpression())).ToList();
                result.ShouldNotBeNull();
                result.Count.ShouldBe(1);
            }
        }
    }
}