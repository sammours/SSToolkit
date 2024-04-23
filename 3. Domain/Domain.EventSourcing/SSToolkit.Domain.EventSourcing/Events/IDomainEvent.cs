namespace SSToolkit.Domain.EventSourcing.Events
{
    public interface IDomainEvent<TId>
    {
        int AggregateVersion { get; set; }

        Guid EventId { get; }

        DateTimeOffset Timestamp { get; }

        TId AggregateId { get; }
    }
}
