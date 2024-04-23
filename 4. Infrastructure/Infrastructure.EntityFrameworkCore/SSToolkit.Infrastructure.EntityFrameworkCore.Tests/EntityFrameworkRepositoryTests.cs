namespace SSToolkit.Infrastructure.EntityFrameworkCore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Shouldly;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Specifications;
    using Xunit;

    public class EntityFrameworkRepositoryTests
    {
        [Fact]
        public async Task FindAll_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var findResults = await sut.FindAllAsync().ConfigureAwait(false);
                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(10);
            }
        }

        [Fact]
        public async Task FindAll_WithSpecifications_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var findResultsWithSpecification = await sut.FindAllAsync(new Specification<Stub>(x => x.FirstName == "FirstName 1" || x.FirstName == "FirstName 2"));
                findResultsWithSpecification.ShouldNotBeNull();
                findResultsWithSpecification.Count().ShouldBe(2);

                var findResultsWithSpecifications = await sut.FindAllAsync(new[] { new Specification<Stub>(x => x.FirstName == "FirstName 1" || x.FirstName == "FirstName 2") });
                findResultsWithSpecifications.ShouldNotBeNull();
                findResultsWithSpecifications.Count().ShouldBe(2);

                var findResultsWithTenantSpecfication = await sut.FindAllAsync(
                    new Specification<Stub>(x => x.FirstName == "FirstName 1" || x.FirstName == "FirstName 2"),
                    new FindOptions<Stub>() { Take = 5 });

                findResultsWithTenantSpecfication.ShouldNotBeNull();
                findResultsWithTenantSpecfication.Count().ShouldBe(2);
            }
        }

        [Fact]
        public async Task FindAll_WithAndSpecification_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);
                var id = context.Entities.FirstOrDefault(x => x.FirstName == "FirstName 1").Id;

                var findResults = await sut.FindAllAsync(
                    new Specification<Stub>(x => x.FirstName == "FirstName 1")
                    .And(new Specification<Stub>(x => x.Id == id)));

                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(1);
                findResults.ToArray()[0].FirstName.ShouldBe("FirstName 1");
            }
        }

        [Fact]
        public async Task FindAll_WithOrSpecification_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var findResults = await sut.FindAllAsync(
                    new Specification<Stub>(x => x.FirstName == "FirstName 1")
                    .Or(new Specification<Stub>(x => x.FirstName == "FirstName 2")));

                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(2);
                findResults.OrderBy(x => x.FirstName).ToArray()[0].FirstName.ShouldBe("FirstName 1");
                findResults.OrderBy(x => x.FirstName).ToArray()[1].FirstName.ShouldBe("FirstName 2");
            }
        }

        [Fact]
        public async Task FindAll_WithNotSpecification_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var item1 = context.Entities.ToArray()[0];
                var item2 = context.Entities.ToArray()[1];
                var findResults = await sut.FindAllAsync(
                    new Specification<Stub>(x => x.FirstName == item1.FirstName)
                        .Or(new Specification<Stub>(x => x.FirstName == item2.FirstName))
                        .Not());

                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(8);
                findResults.ShouldNotContain(item1);
                findResults.ShouldNotContain(item2);
            }
        }

        [Fact]
        public async Task FindById_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var entity = context.Entities.FirstOrDefault();

                var findResult = await sut.FindOneAsync(entity.Id);
                findResult.ShouldNotBeNull();
                findResult.Id.ShouldBe(entity.Id);
                findResult.FirstName.ShouldBe(entity.FirstName);

                var findResultUnknownId = await sut.FindOneAsync(Guid.NewGuid());
                findResultUnknownId.ShouldBeNull();
            }
        }

        [Fact]
        public async Task Insert_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var entity = new Stub
                {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName 11"
                };

                var result = await sut.UpsertAsync(entity).ConfigureAwait(false);
                result.action.ShouldBe(RepositoryActionResult.Inserted);
                result.entity.Id.ShouldBe(entity.Id);
                result.entity.FirstName.ShouldBe("FirstName 11");

                var findResult = await sut.FindOneAsync(entity.Id).ConfigureAwait(false);
                findResult.ShouldNotBeNull();
                findResult.Id.ShouldBe(entity.Id);
                findResult.FirstName.ShouldBe("FirstName 11");

                var findResults = await sut.FindAllAsync().ConfigureAwait(false);
                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(11);
            }
        }

        [Fact]
        public async Task Update_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);
                var oldEntity = context.Entities.FirstOrDefault();

                var entity = new Stub
                {
                    Id = oldEntity.Id,
                    FirstName = "FirstName 20",
                    LastName = oldEntity.LastName,
                    Age = oldEntity.Age
                };

                var result = await sut.UpsertAsync(entity).ConfigureAwait(false);
                result.action.ShouldBe(RepositoryActionResult.Updated);
                result.entity.Id.ShouldBe(oldEntity.Id);
                result.entity.FirstName.ShouldBe("FirstName 20");

                var findResult = await sut.FindOneAsync(entity.Id).ConfigureAwait(false);
                findResult.ShouldNotBeNull();
                result.entity.Id.ShouldBe(oldEntity.Id);
                result.entity.FirstName.ShouldBe("FirstName 20");

                var findResults = await sut.FindAllAsync().ConfigureAwait(false);
                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(10);
            }
        }

        [Fact]
        public async Task Delete_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                var id1 = context.Entities.FirstOrDefault().Id;
                var id2 = context.Entities.LastOrDefault().Id;

                sut.DeleteAsync(id1).Wait();
                sut.DeleteAsync(new Stub { Id = id2 }).Wait();

                var findResults = await sut.FindAllAsync().ConfigureAwait(false);
                findResults.ShouldNotBeNull();
                findResults.Count().ShouldBe(8);
                findResults.ShouldNotContain(context.Entities.FirstOrDefault(x => x.Id == id1));
                findResults.ShouldNotContain(context.Entities.FirstOrDefault(x => x.Id == id2));
            }
        }

        [Fact]
        public async Task Count_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);

                (await sut.CountAsync()).ShouldBe(10);
            }
        }

        [Fact]
        public async Task Count_WithSpecification_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);
                var specification = new Specification<Stub>(x => x.Age > 5);

                (await sut.CountAsync(specification)).ShouldBe(5);
            }
        }

        [Fact]
        public async Task Count_WithSpecifications_Test()
        {
            using (var context = new StubDbContext())
            {
                var sut = this.CreateRepository(context);
                var specifications = new List<Specification<Stub>>
                {
                    new Specification<Stub>(x => x.FirstName == "FirstName 5" || x.FirstName == "FirstName 6"),
                    new Specification<Stub>(x => x.Age == 5)
                };

                (await sut.CountAsync(specifications)).ShouldBe(1);
            }
        }

        private EntityFrameworkRepository<Stub> CreateRepository(StubDbContext dbContext)
            => EntityFrameworkRepositoryFactory.Create<Stub>(dbContext)
                as EntityFrameworkRepository<Stub>;
    }
}
