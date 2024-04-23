namespace SSToolkit.Application.Commands.Core.Commands
{
    using System;
    using MediatR;

    public interface ICommandRequest : IRequest
    {
        /// <summary>
        /// Gets or sets a unique request Id
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// Gets or sets the time stamp
        /// </summary>
        DateTimeOffset Timestamp { get; }
    }

    public interface ICommandRequest<out TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Gets or sets a unique request Id
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// Gets or sets the time stamp
        /// </summary>
        DateTimeOffset Timestamp { get; }
    }

    public interface ICommandRequest<TModel, out TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Gets or sets a unique request Id
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// Gets or sets the time stamp
        /// </summary>
        DateTimeOffset Timestamp { get; }

        /// <summary>
        /// Gets or sets request model
        /// </summary>
        public TModel Model { get; }
    }
}
