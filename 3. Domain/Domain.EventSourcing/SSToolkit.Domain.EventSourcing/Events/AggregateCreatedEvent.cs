namespace SSToolkit.Domain.EventSourcing.Events
{
    public class AggregateCreatedEvent : AggregateEvent, IAggregateEvent
    {
        public AggregateCreatedEvent(Guid aggregateId)
            : base(aggregateId, 1)
        {
        }
    }
}
