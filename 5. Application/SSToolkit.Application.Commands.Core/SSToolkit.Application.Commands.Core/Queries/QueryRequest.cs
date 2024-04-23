namespace SSToolkit.Application.Commands.Core.Queries
{
    using System;

    public abstract class QueryRequest<TResponse> : IQueryRequest<QueryResponse<TResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRequest{TResponse}"/> class.
        /// </summary>
        protected QueryRequest()
        {
            this.RequestId = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRequest{TResponse}"/> class.
        /// <paramref name="requestId">The request id</paramref>
        /// </summary>
        protected QueryRequest(Guid requestId)
        {
            this.RequestId = requestId;
        }

        /// <summary>
        /// Gets or sets a unique request Id
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
