namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using NSubstitute;
    using Shouldly;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Specifications;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class CosmosDbRepositoryTests
    {
        private Container container;
        private ICosmosDbRepository<Stub> repository;

        public CosmosDbRepositoryTests()
        {
            this.container = Substitute.For<Container>();
            this.repository = new TestCosmosDbRepository<Stub>(new CosmosDbRepositoryOptions<Stub>
            {
                Client = Substitute.For<CosmosClient>(),
                PartitionKeyExpression = x => x.Age,
                Container = this.container,
            });
        }

        [Fact]
        public async Task CountAsync_Test()
        {
            var list = this.GetMockData();
            this.container.GetItemLinqQueryable<Stub>(requestOptions: default).ReturnsForAnyArgs(list.AsQueryable());

            var count = await this.repository.CountAsync().ConfigureAwait(false);
            count.ShouldBe(5);

            count = await this.repository.CountAsync(new Specification<Stub>(x => x.Age >= 20)).ConfigureAwait(false);
            count.ShouldBe(3);

            count = await this.repository.CountAsync(new List<Specification<Stub>>
            {
                new Specification<Stub>(x => x.Age <= 30),
                new Specification<Stub>(x => x.Location == "Italy")
            }).ConfigureAwait(false);

            count.ShouldBe(2);
        }

        [Fact]
        public async Task FindOneAsync_Test()
        {
            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list);
            this.container.GetItemQueryIterator<Stub>(queryDefinition: Arg.Any<QueryDefinition>(), continuationToken: Arg.Any<string?>(), requestOptions: Arg.Any<QueryRequestOptions>())
                .ReturnsForAnyArgs(feedIterator);

            var result = await this.repository.FindOneAsync(list.First().Id.ToGuid()).ConfigureAwait(false);
            result.ShouldNotBeNull();
            result.FirstName.ShouldBe("FirstName1");

            this.container.GetItemLinqQueryable<Stub>(requestOptions: default).ReturnsForAnyArgs(list.AsQueryable());
        }

        [Fact]
        public async Task FindAllAsync_Test()
        {
            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list);
            this.container.GetItemQueryIterator<Stub>(queryDefinition: Arg.Any<QueryDefinition>(), continuationToken: Arg.Any<string?>(), requestOptions: Arg.Any<QueryRequestOptions>())
                .ReturnsForAnyArgs(feedIterator);

            var result = await this.repository.FindAllAsync("select * from c where c.FirstName = @firstName", new List<DbParameter>
            {
                new DbParameter("firstName", "FirstName4")
            }).ConfigureAwait(false);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(5);

            this.container.Received(1).GetItemQueryIterator<Stub>(queryDefinition: Arg.Is<QueryDefinition>(x =>
                    x.QueryText == "select * from c where c.FirstName = @firstName" &&
                    x.GetQueryParameters().First().Name == "@firstName" &&
                    x.GetQueryParameters().First().Value.ToString() == "FirstName4"),
                            continuationToken: Arg.Any<string>(), requestOptions: Arg.Any<QueryRequestOptions>());
        }

        [Fact]
        public async Task ExistsAsync_Test()
        {
            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list);
            this.container.GetItemQueryIterator<Stub>(queryDefinition: default, continuationToken: default, requestOptions: default)
                .ReturnsForAnyArgs(feedIterator);

            var exists = await this.repository.ExistsAsync(list.First().Id).ConfigureAwait(false);
            exists.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteAsync_Test()
        {
            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list.First().AsList());
            this.container.GetItemQueryIterator<Stub>(queryDefinition: default, continuationToken: default, requestOptions: default)
                .ReturnsForAnyArgs(feedIterator);

            var response = Substitute.For<ItemResponse<Stub>>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            this.container.DeleteItemAsync<Stub>(
                    list.First().Id,
                    partitionKey: Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>())
                .Returns(response);

            var action = await this.repository.DeleteAsync((Guid)default).ConfigureAwait(false);
            action.ShouldBe(RepositoryActionResult.None);

            action = await this.repository.DeleteAsync(list.First().Id).ConfigureAwait(false);
            action.ShouldBe(RepositoryActionResult.Deleted);
        }

        [Fact]
        public async Task DeleteAsync_Entity_Test()
        {
            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list.First().AsList());
            this.container.GetItemQueryIterator<Stub>(queryDefinition: default, continuationToken: default, requestOptions: default)
                .ReturnsForAnyArgs(feedIterator);

            var response = Substitute.For<ItemResponse<Stub>>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            this.container.DeleteItemAsync<Stub>(
                    list.First().Id,
                    partitionKey: Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>())
                .Returns(response);

            var action = await this.repository.DeleteAsync(list.First()).ConfigureAwait(false);
            action.ShouldBe(RepositoryActionResult.Deleted);
        }

        [Fact]
        public async Task InsertASync_Test()
        {
            var newItem = new Stub
            {
                Age = 50,
                FirstName = "FirstName6",
                Location = "Italy"
            };

            var list = this.GetMockData();

            var feedIterator = this.GetFeedIteratorMock(list.First().AsList());
            this.container.GetItemQueryIterator<Stub>(queryDefinition: default, continuationToken: default, requestOptions: default)
                .ReturnsForAnyArgs(feedIterator);

            var response = Substitute.For<ItemResponse<Stub>>();
            response.Resource.Returns(newItem);
            this.container.UpsertItemAsync<Stub>(
                    Arg.Any<Stub>(),
                    partitionKey: Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>())
                .Returns(response);

            var stub = await this.repository.InsertAsync(newItem).ConfigureAwait(false);
            stub.ShouldNotBeNull();
            stub.Age.ShouldBe(newItem.Age);
            stub.FirstName.ShouldBe(newItem.FirstName);
            stub.Location.ShouldBe(newItem.Location);

            await this.container.Received(1).UpsertItemAsync(Arg.Is<Stub>(x =>
               x.Age == newItem.Age &&
               x.FirstName == newItem.FirstName &&
               x.Location == newItem.Location), Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>()).AnyContext();
        }

        [Fact]
        public async Task UpdateASync_Test()
        {
            var list = this.GetMockData();
            var newItem = new Stub
            {
                Id = list.First().Id,
                Age = 50,
                FirstName = "FirstName6",
                Location = "Italy"
            };

            var feedIterator = this.GetFeedIteratorMock(list.First().AsList());
            this.container.GetItemQueryIterator<Stub>(queryDefinition: default, continuationToken: default, requestOptions: default)
                .ReturnsForAnyArgs(feedIterator);

            var response = Substitute.For<ItemResponse<Stub>>();
            response.Resource.Returns(newItem);
            this.container.UpsertItemAsync<Stub>(
                    Arg.Any<Stub>(),
                    partitionKey: Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>())
                .Returns(response);

            var stub = await this.repository.UpdateAsync(newItem).ConfigureAwait(false);
            stub.ShouldNotBeNull();
            stub.Age.ShouldBe(newItem.Age);
            stub.FirstName.ShouldBe(newItem.FirstName);
            stub.Location.ShouldBe(newItem.Location);

            await this.container.Received(1).UpsertItemAsync(Arg.Is<Stub>(x =>
               x.Id == newItem.Id &&
               x.Age == newItem.Age &&
               x.FirstName == newItem.FirstName &&
               x.Location == newItem.Location), Arg.Any<PartitionKey>(), cancellationToken: Arg.Any<CancellationToken>()).AnyContext();
        }

        private List<Stub> GetMockData()
         => new()
         {
             new Stub() { Id = Guid.NewGuid().ToString(), FirstName = "FirstName1", Age = 20, Location = "Germany" },
             new Stub() { Id = Guid.NewGuid().ToString(), FirstName = "FirstName2", Age = 40, Location = "Germany" },
             new Stub() { Id = Guid.NewGuid().ToString(), FirstName = "FirstName3", Age = 26, Location = "Germany" },
             new Stub() { Id = Guid.NewGuid().ToString(), FirstName = "FirstName4", Age = 18, Location = "Italy" },
             new Stub() { Id = Guid.NewGuid().ToString(), FirstName = "FirstName5", Age = 15, Location = "Italy" },
         };

        private FeedIterator<Stub> GetFeedIteratorMock(List<Stub> list)
        {
            var feedResponse = Substitute.For<FeedResponse<Stub>>();
            var feedIterator = Substitute.For<FeedIterator<Stub>>();

            feedResponse.Resource.Returns(list);
            feedIterator.HasMoreResults.Returns(true);
            feedIterator.ReadNextAsync(cancellationToken: default).ReturnsForAnyArgs(feedResponse)
                .AndDoes(x => feedIterator.HasMoreResults.Returns(false));
            return feedIterator;
        }
    }
}