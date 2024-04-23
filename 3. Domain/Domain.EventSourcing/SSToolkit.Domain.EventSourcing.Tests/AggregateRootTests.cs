namespace SSToolkit.Domain.EventSourcing.Tests
{
    using SSToolkit.Domain.EventSourcing.Events;

    public class AggregateRootTests
    {
        [Fact]
        public void AggregateRoot_Create_Test()
        {
            var product = new Product(new ProductHasBeenCreated(Guid.NewGuid()));

            Assert.Equal(5, product.Price);

            product.ChangePrice(100);

            Assert.Equal(100, product.Price);
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class Product : AggregateRoot
    {
        public Product(ProductHasBeenCreated createEvent)
            : base(createEvent)
        {
        }

        public int Price { get; private set; }

        public void ChangePrice(int newPrice)
        {
            this.ReceiveEvent(new ProductPriceHasBeenChanged(newPrice, this.Id, this.NextVersion()));
        }

        private void Apply(ProductHasBeenCreated @event)
        {
            this.Price = 5;
        }

        private void Apply(ProductPriceHasBeenChanged @event)
        {
            this.Price = @event.NewPrice;
        }
    }

    public class ProductHasBeenCreated : AggregateCreatedEvent, IAggregateEvent
    {
        public ProductHasBeenCreated(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }

    public class ProductPriceHasBeenChanged : AggregateEvent, IAggregateEvent
    {
        public ProductPriceHasBeenChanged(int newPrice, Guid aggregateId, int nextVersion)
            : base(aggregateId, nextVersion)
        {
            this.NewPrice = newPrice;
        }

        public int NewPrice { get; private set; }
    }
}
#pragma warning restore SA1402 // File may only contain a single type