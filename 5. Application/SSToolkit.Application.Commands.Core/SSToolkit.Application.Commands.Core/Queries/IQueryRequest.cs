namespace SSToolkit.Application.Commands.Core.Queries
{
    using System;
    using MediatR;

    public interface IQueryRequest<out TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Gets or sets a unique request Id
        /// </summary>
        Guid RequestId { get; }
    }
}
