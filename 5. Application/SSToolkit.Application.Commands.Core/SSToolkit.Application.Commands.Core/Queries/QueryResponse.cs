namespace SSToolkit.Application.Commands.Core.Queries
{
    public class QueryResponse<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse{TResponse}"/> class.
        /// <br>Use when Completed</br>
        /// </summary>
        public QueryResponse()
        {
            this.Status = ResponseStatus.Completed;
            this.Result = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse{TResponse}"/> class.
        /// <br>Use when Complete</br>
        /// </summary>
        /// <param name="result">The result</param>
        public QueryResponse(TResponse result)
        {
            this.Result = result;
            this.Status = ResponseStatus.Completed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse{TResponse}"/> class.
        /// <br>Use when Cancelled</br>
        /// </summary>
        /// <param name="cancellationReason">The cancellation reason</param>
        public QueryResponse(string cancellationReason)
        {
            this.Status = ResponseStatus.Cancelled;
            this.CancellationReason = cancellationReason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse{TResponse}"/> class.
        /// <br>Use when custom status <see cref="ResponseStatus"/></br>
        /// </summary>
        /// <param name="status">The stats</param>
        /// <param name="cancellationReason">The cancellation reason</param>
        public QueryResponse(ResponseStatus status, string cancellationReason)
        {
            this.Status = status;
            this.CancellationReason = cancellationReason;
        }

        /// <summary>
        /// Gets or sets the result
        /// </summary>
        public TResponse? Result { get; set; }

        /// <summary>
        /// Returns <c>true</c> when Status is Cancelled
        /// </summary>
        public bool IsCancelled => this.Status == ResponseStatus.Cancelled;

        /// <summary>
        /// Gets the status
        /// </summary>
        public ResponseStatus Status { get; } = ResponseStatus.Completed;

        /// <summary>
        /// Gets cancellation reason
        /// </summary>
        public string? CancellationReason { get; } = null;
    }
}
