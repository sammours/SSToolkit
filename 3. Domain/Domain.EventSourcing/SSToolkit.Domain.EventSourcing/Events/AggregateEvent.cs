namespace SSToolkit.Domain.EventSourcing.Events
{
    public class AggregateEvent : IAggregateEvent
    {
        public AggregateEvent(Guid aggregateId, int version)
        {
            this.AggregateVersion = version;
            this.EventId = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
            this.AggregateId = aggregateId;
        }

        public Guid EventId { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public Guid AggregateId { get; private set; }

        public int AggregateVersion { get; set; }
    }
}
