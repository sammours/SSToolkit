namespace SSToolkit.Domain.Repositories.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using SSToolkit.Domain.Repositories.Decorators;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Domain.Repositories.Specifications;
    using Xunit;

    public class LoggingRepositoryDecoratorTests
    {
        private readonly LoggingRepositoryDecorator<StubEntity> decorated;
        private readonly IRepository<StubEntity> decoratee;
        private readonly ILogger<IRepository<StubEntity>> logger;

        public LoggingRepositoryDecoratorTests()
        {
            this.logger = Substitute.For<ILogger<IRepository<StubEntity>>>();
            this.decoratee = Substitute.For<IRepository<StubEntity>>();
            this.decorated = new LoggingRepositoryDecorator<StubEntity>(this.decoratee, this.logger);
        }

        [Fact]
        public void DeleteAsync_Id_Test()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            this.decorated.DeleteAsync(id).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start deleting {typeof(StubEntity)} {id}");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {id} is deleted");
        }

        [Fact]
        public void DeleteAsync_Entity_Test()
        {
            // Arrange
            var stub = new StubEntity { Id = Guid.NewGuid() };

            // Act
            this.decorated.DeleteAsync(stub).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start deleting {typeof(StubEntity)} {stub.Id}");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {stub.Id} is deleted");
        }

        [Fact]
        public void ExistsAsync_Test()
        {
            // Arrange
            var id = Guid.NewGuid();
            this.decoratee.ExistsAsync(id).Returns(true);

            // Act
            this.decorated.ExistsAsync(id).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start checking if {typeof(StubEntity)} {id} exists");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {id} exists: True");
        }

        [Fact]
        public void FindAllAsync_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>();

            // Act
            this.decorated.FindAllAsync(options).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation($"Get all {typeof(StubEntity)}.");
        }

        [Fact]
        public void FindAllAsync_WithTakeSkip_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>()
            {
                Take = 10,
                Skip = 1
            };

            // Act
            this.decorated.FindAllAsync(options).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}. Take: {1}. Skip: {2}.",
                        typeof(StubEntity), options.Take, options.Skip));
        }

        [Fact]
        public void FindAllAsync_WithOrders_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>()
            {
                Order = new OrderByOption<StubEntity>(x => x.Age),
                Orders = new List<OrderByOption<StubEntity>>()
                {
                    new OrderByOption<StubEntity>(x => x.FirstName, OrderByDirection.Desc)
                }
            };

            // Act
            this.decorated.FindAllAsync(options).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}.", typeof(StubEntity)));
            this.logger.Received(1).LogInformation(string.Format("Order: {0}. Order: {1}.", options.GetOrders().Select(x => x.ToString()).ToArray()));
        }

        [Fact]
        public void FindAllAsync_WithIncludes_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>()
            {
                Include = new IncludeOption<StubEntity>(x => x.Age),
                Includes = new List<IncludeOption<StubEntity>>()
                {
                    new IncludeOption<StubEntity>(x => x.FirstName)
                }
            };

            // Act
            this.decorated.FindAllAsync(options).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}.", typeof(StubEntity)));
            this.logger.Received(1).LogInformation(string.Format("Include: {0}. Include: {1}.", options.GetIncludes().Select(x => x.ToString()).ToArray()));
        }

        [Fact]
        public void FindAllAsync_Specification_Test()
        {
            // Arrange
            var specification = new Specification<StubEntity>(x => x.FirstName == "Name");

            // Act
            this.decorated.FindAllAsync(specification).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}. Specification: {1}.", typeof(StubEntity), specification.ToString()));
        }

        [Fact]
        public void FindAllAsync_Specifications_Test()
        {
            // Arrange
            var specifications = new List<Specification<StubEntity>>
            {
                new Specification<StubEntity>(x => x.FirstName == "Name"),
                new Specification<StubEntity>(x => x.LastName == "Name")
            };

            // Act
            this.decorated.FindAllAsync(specifications).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}. Specification: {1}. Specification: {2}.",
                typeof(StubEntity), specifications.First().ToString(), specifications.Last().ToString()));
        }

        [Fact]
        public void FindAllAsync_All_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>()
            {
                Take = 10,
                Skip = 1,
                Order = new OrderByOption<StubEntity>(x => x.Age),
                Orders = new List<OrderByOption<StubEntity>>()
                {
                    new OrderByOption<StubEntity>(x => x.FirstName, OrderByDirection.Desc)
                },
                Include = new IncludeOption<StubEntity>(x => x.Age),
                Includes = new List<IncludeOption<StubEntity>>()
                {
                    new IncludeOption<StubEntity>(x => x.FirstName)
                }
            };
            var specifications = new List<Specification<StubEntity>>
            {
                new Specification<StubEntity>(x => x.FirstName == "Name"),
                new Specification<StubEntity>(x => x.LastName == "Name")
            };

            // Act
            this.decorated.FindAllAsync(specifications, options).Wait();

            // Assert
            this.logger.Received(3);
            this.logger.Received(1).LogInformation(string.Format("Get all {0}. Specification: {1}. Specification: {2}. Take: {3}. Skip: {4}.",
                typeof(StubEntity), specifications.First().ToString(), specifications.Last().ToString(), options.Take, options.Skip));
            this.logger.Received(1).LogInformation(string.Format("Order: {0}. Order: {1}.", options.GetOrders().Select(x => x.ToString()).ToArray()));
            this.logger.Received(1).LogInformation(string.Format("Include: {0}. Include: {1}.", options.GetIncludes().Select(x => x.ToString()).ToArray()));
        }

        [Fact]
        public void FindOneAsync_Test()
        {
            // Arrange
            var options = new FindOptions<StubEntity>();

            // Act
            this.decorated.FindOneAsync(options).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation($"Get first {typeof(StubEntity)}.");
        }

        [Fact]
        public void FindOneAsync_Id_Test()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            this.decorated.FindOneAsync(id).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation($"Get {typeof(StubEntity)} by {id}.");
        }

        [Fact]
        public void InsertAsync_Test()
        {
            // Arrange
            var stub = new StubEntity { Id = Guid.NewGuid() };

            // Act
            this.decoratee.InsertAsync(Arg.Any<StubEntity>()).Returns(stub);
            this.decorated.InsertAsync(stub).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start inserting {typeof(StubEntity)}");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {stub.Id} is inserted");
        }

        [Fact]
        public void UpdateAsync_Test()
        {
            // Arrange
            var stub = new StubEntity { Id = Guid.NewGuid() };

            // Act
            this.decoratee.UpdateAsync(Arg.Any<StubEntity>()).Returns(stub);
            this.decorated.UpdateAsync(stub).Wait();

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start updating {typeof(StubEntity)}");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {stub.Id} is updated");
        }

        [Fact]
        public void UpsertAsync_Test()
        {
            // Arrange
            var stub = new StubEntity { Id = Guid.NewGuid() };

            // Act
            this.decoratee.UpsertAsync(Arg.Any<StubEntity>()).Returns((stub, RepositoryActionResult.Inserted));
            var result = this.decorated.UpsertAsync(stub).Result;

            // Assert
            this.logger.Received(2);
            this.logger.Received(1).LogInformation($"Start upserting {typeof(StubEntity)}");
            this.logger.Received(1).LogInformation($"{typeof(StubEntity)} {stub.Id} is {result.action}");
        }

        [Fact]
        public void CountAsync_Test()
        {
            // Arrange

            // Act
            this.decoratee.CountAsync().Returns(1);
            this.decorated.CountAsync().Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation($"Get counts fors {typeof(StubEntity)}.");
        }

        [Fact]
        public void CountAsync_WithSpecification_Test()
        {
            // Arrange
            var specification = new Specification<StubEntity>(x => x.FirstName == "Name");

            // Act
            this.decoratee.CountAsync().Returns(1);
            this.decorated.CountAsync(specification).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation($"Get counts fors {typeof(StubEntity)}. Specification: {specification}.");
        }

        [Fact]
        public void CountAsync_WithSpecifications_Test()
        {
            // Arrange
            var specifications = new List<Specification<StubEntity>>
            {
                new Specification<StubEntity>(x => x.FirstName == "Name"),
                new Specification<StubEntity>(x => x.LastName == "Name")
            };

            // Act
            this.decoratee.CountAsync().Returns(1);
            this.decorated.CountAsync(specifications).Wait();

            // Assert
            this.logger.Received(1);
            this.logger.Received(1).LogInformation(string.Format("Get counts fors {0}. Specification: {1}. Specification: {2}.",
                    typeof(StubEntity), specifications.First().ToString(), specifications.Last().ToString()));
        }

        public class StubEntity : Entity
        {
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public long Age { get; set; }
        }
    }
}