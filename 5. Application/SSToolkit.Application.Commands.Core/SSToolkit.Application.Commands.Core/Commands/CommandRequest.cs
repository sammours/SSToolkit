namespace SSToolkit.Application.Commands.Core.Commands
{
    using System;
    using MediatR;

    public abstract class CommandRequest : ICommandRequest<CommandResponse>, IBaseRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRequest"/> class.
        /// </summary>
        protected CommandRequest()
        {
            this.CommandId = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the unique command request Id
        /// </summary>
        public Guid CommandId { get; private set; }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public abstract class CommandRequest<TResponse> : ICommandRequest<CommandResponse<TResponse>>, IBaseRequest
#pragma warning restore SA1402 // File may only contain a single type
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRequest{TResponse}"/> class.
        /// </summary>
        protected CommandRequest()
        {
            this.CommandId = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the unique command request Id
        /// </summary>
        public Guid CommandId { get; private set; }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }
    }
}
