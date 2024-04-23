namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class StubDbContext : BaseDbContext
    {
        public StubDbContext()
            : base(new DbContextOptionsBuilder<StubDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options)
        {
            this.Seeds();
        }

        public DbSet<Stub> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("stub");
        }

        private void Seeds()
        {
            var entities = new List<Stub>();
            for (var i = 1; i <= 10; i++)
            {
                var stub = new Stub()
                {
                    Id = Guid.NewGuid(),
                    Age = i,
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    NestedStubs = new List<NestedStub>
                    {
                        new NestedStub()
                        {
                            Id = Guid.NewGuid(),
                            Value = $"NestedStub {i}",
                            SecondLevelNestedStubs = new List<SecondLevelNestedStub>
                            {
                                new SecondLevelNestedStub()
                                {
                                    Id = Guid.NewGuid(),
                                    Value = $"SecondLevelNestedStub {i}"
                                }
                            }
                        }
                    },
                    NestedStubs2 = new List<NestedStub>
                    {
                        new NestedStub()
                        {
                            Id = Guid.NewGuid(),
                            Value = $"NestedStub2 {i}",
                            SecondLevelNestedStubs = new List<SecondLevelNestedStub>
                            {
                                new SecondLevelNestedStub()
                                {
                                    Id = Guid.NewGuid(),
                                    Value = $"SecondLevelNestedStub2 {i}"
                                }
                            }
                        }
                    }
                };

                entities.Add(stub);
            }

            this.Entities.AddRange(entities);
            this.SaveChanges();
        }
    }
}
