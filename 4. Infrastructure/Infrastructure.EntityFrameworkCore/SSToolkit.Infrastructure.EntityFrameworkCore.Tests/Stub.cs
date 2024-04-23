namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System;
    using System.Collections.Generic;
    using SSToolkit.Domain.Repositories.Model;

    public class Stub : Entity<Guid>, IEntity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<NestedStub> NestedStubs { get; set; }

        public ICollection<NestedStub> NestedStubs2 { get; set; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class NestedStub : Entity<Guid>, IEntity<Guid>
#pragma warning restore SA1402 // File may only contain a single type
    {
        public string Value { get; set; }

        public ICollection<SecondLevelNestedStub> SecondLevelNestedStubs { get; set; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class SecondLevelNestedStub : Entity<Guid>, IEntity<Guid>
#pragma warning restore SA1402 // File may only contain a single type
    {
        public string Value { get; set; }
    }
}
