namespace SSToolkit.Domain.Repositories.Tests
{
    using Shouldly;
    using SSToolkit.Domain.Repositories.Specifications;
    using Xunit;

    public class SpecificationsTests
    {
        [Fact]
        public void IsSatisfiedBy_Test()
        {
            new Specification<StubEntity>(e => e.FirstName == "Jack")
                .IsSatisfiedBy(new StubEntity("Jack", "Black", 30))
                .ShouldBeTrue();

            new Specification<StubEntity>(e => e.Age == long.MaxValue)
                .IsSatisfiedBy(new StubEntity("Jack", "Black", long.MaxValue))
                .ShouldBeTrue();

            new Specification<StubEntity>(e => e.FirstName == "Jack")
                .IsSatisfiedBy(new StubEntity("Not Jack", "Black", long.MaxValue))
                .ShouldBeFalse();
        }

        [Fact]
        public void And_Test()
        {
            var leftSpecification = new Specification<StubEntity>(e => e.FirstName == "Jack");
            var rightSpecification = new Specification<StubEntity>(e => e.LastName == "Black");

            leftSpecification.And(leftSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.FirstName == \"Jack\") AndAlso (Param_0.FirstName == \"Jack\"))");

            leftSpecification.And(leftSpecification)
                .ToPredicate()(new StubEntity("Jack", "Black", 30))
                .ShouldBeTrue();

            leftSpecification.And(leftSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeTrue();

            leftSpecification.And(leftSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeFalse();

            leftSpecification.And(leftSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();

            rightSpecification.And(rightSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.LastName == \"Black\") AndAlso (Param_0.LastName == \"Black\"))");

            rightSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Black", 30))
                .ShouldBeTrue();

            rightSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeTrue();

            rightSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeFalse();

            rightSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();

            leftSpecification.And(rightSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.FirstName == \"Jack\") AndAlso (Param_0.LastName == \"Black\"))");

            leftSpecification.And(rightSpecification)
                 .ToPredicate()(new StubEntity("Jack", "Black", 30))
                 .ShouldBeTrue();

            leftSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeFalse();

            leftSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeFalse();

            leftSpecification.And(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();
        }

        [Fact]
        public void Or_Test()
        {
            var leftSpecification = new Specification<StubEntity>(e => e.FirstName == "Jack");
            var rightSpecification = new Specification<StubEntity>(e => e.LastName == "Black");

            leftSpecification.Or(leftSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.FirstName == \"Jack\") OrElse (Param_0.FirstName == \"Jack\"))");

            leftSpecification.Or(leftSpecification)
                .ToPredicate()(new StubEntity("Jack", "Black", 30))
                .ShouldBeTrue();

            leftSpecification.Or(leftSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeTrue();

            leftSpecification.Or(leftSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeFalse();

            leftSpecification.Or(leftSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();

            rightSpecification.Or(rightSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.LastName == \"Black\") OrElse (Param_0.LastName == \"Black\"))");

            rightSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Black", 30))
                .ShouldBeTrue();

            rightSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeTrue();

            rightSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeFalse();

            rightSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();

            leftSpecification.Or(rightSpecification)
                .ToString()
                .ShouldBe("Param_0 => ((Param_0.FirstName == \"Jack\") OrElse (Param_0.LastName == \"Black\"))");

            leftSpecification.Or(rightSpecification)
                 .ToPredicate()(new StubEntity("Jack", "Black", 30))
                 .ShouldBeTrue();

            leftSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Black", 30))
                .ShouldBeTrue();

            leftSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Jack", "Not Black", 30))
                .ShouldBeTrue();

            leftSpecification.Or(rightSpecification)
                .ToPredicate()(new StubEntity("Not Jack", "Not Black", 30))
                .ShouldBeFalse();
        }

        [Fact]
        public void Not_Test()
        {
            var specification = new Specification<StubEntity>(e => e.FirstName == "Jack");

            var stub = new StubEntity("Jack", "Black", 30);

            specification.ToPredicate()(stub).ShouldBeTrue();
            specification.Not().ToPredicate()(stub).ShouldBeFalse();
            specification.Not().Not().ToPredicate()(stub).ShouldBeTrue();
        }

        public class StubEntity
        {
            public StubEntity(string firstName, string lastName, long age)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Age = age;
            }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public long Age { get; set; }
        }
    }
}