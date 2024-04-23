namespace SSToolkit.Application.Commands.Core.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, QueryResponse<TResponse>>
        where TRequest : QueryRequest<TResponse>, IQueryRequest<QueryResponse<TResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler{TRequest, TResponse}"/> class.
        /// </summary>
        protected QueryHandler()
        {
        }

        /// <summary>
        /// Handle method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<QueryResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new QueryResponse<TResponse>("Request cannot be null");
            }

            var response = await this.Process(request, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Process method (Should be implemented)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<QueryResponse<TResponse>> Process(TRequest request, CancellationToken cancellationToken);
    }
}
