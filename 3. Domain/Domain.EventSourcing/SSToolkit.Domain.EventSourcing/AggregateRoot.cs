/*
 * List of events
 * Version
 * Add new Event to list
 * Apply
 * Increase Version
 */


namespace SSToolkit.Domain.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;
    using System.Xml.Linq;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using SSToolkit.Domain.EventSourcing.Events;

    public abstract class AggregateRoot
    {
        private readonly IList<IAggregateEvent> events = new List<IAggregateEvent>();

        protected AggregateRoot(IAggregateEvent @event)
        {
            ArgumentNullException.ThrowIfNull($"Cannot apply null event: {nameof(@event)} to the event store");
            this.Id = @event.AggregateId;
            this.ReceiveEvent(@event);
        }

        public Guid Id { get; private set; }

        public int Version { get; private set; }

        protected int NextVersion() => this.Version + 1;

        protected void ReceiveEvent(IAggregateEvent @event)
        {
            var nextVersion = this.NextVersion();
            this.ValidateEvent(@event, nextVersion);
            this.CallApplyMethod(@event);
            this.events.Add(@event);
            this.Version = nextVersion;
        }

        protected void ApplyEvent(IAggregateEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException($"Cannot apply null event: {nameof(@event)} to the event store");
            }

            this.CallApplyMethod(@event);
        }

        private void ValidateEvent(IAggregateEvent @event, int nextVersion)
        {
            if (@event is null)
            {
                throw new ArgumentNullException($"Cannot apply null event: {nameof(@event)} to the event store");
            }

            if (@event.AggregateId != this.Id)
            {
                throw new ArgumentNullException($"Only events related to the aggregate can be applied. Event Aggregate Id: {@event.AggregateId}. Aggregate: {this.Id}");
            }

            if (@event.AggregateVersion != nextVersion)
            {
                throw new ArgumentNullException($"The aggregate version of the event is not incremental. Event Version: {@event.AggregateVersion}. Should Version: {nextVersion}");
            }
        }

        private void CallApplyMethod(IAggregateEvent @event)
        {
            try
            {
                this.GetType().InvokeMember(
                    "Apply",
                    BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    this,
                    new object[] { @event });
            }
            catch (Exception ex)
            {
                var mEx = new MissingMethodException(
                    $"The Apply method for the event type: {@event.GetType()} has not been found. Make sure to add an Apply with event parameter to specify how the event will be applied.",
                    ex);
                throw mEx;
            }
        }
    }
}
