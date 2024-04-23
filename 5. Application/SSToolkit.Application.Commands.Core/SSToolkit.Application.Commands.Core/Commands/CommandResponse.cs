namespace SSToolkit.Application.Commands.Core.Commands
{
    public class CommandResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse"/> class.
        /// <br>Use when Completed</br>
        /// </summary>
        public CommandResponse()
        {
            this.Status = ResponseStatus.Completed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse"/> class.
        /// <br>Use when Cancelled</br>
        /// </summary>
        /// <param name="cancellationReason">The cancellation reason</param>
        public CommandResponse(string cancellationReason)
        {
            this.Status = ResponseStatus.Cancelled;
            this.CancellationReason = cancellationReason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse"/> class.
        /// <br>Use when custom status <see cref="ResponseStatus"/></br>
        /// </summary>
        /// <param name="status">The stats</param>
        /// <param name="cancellationReason">The cancellation reason</param>
        public CommandResponse(ResponseStatus status, string cancellationReason)
        {
            this.Status = status;
            this.CancellationReason = cancellationReason;
        }

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

#pragma warning disable SA1402 // File may only contain a single type
    public class CommandResponse<TResponse> : CommandResponse
#pragma warning restore SA1402 // File may only contain a single type
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse{TResponse}"/> class.
        /// <br>Use when Completed</br>
        /// </summary>
        /// <param name="result"></param>
        public CommandResponse(TResponse result)
            : base()
        {
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse{TResponse}"/> class.
        /// <br>Use when Cancelled</br>
        /// </summary>
        /// <param name="cancellationReason">The cancellation reason</param>
        public CommandResponse(string cancellationReason)
            : base(cancellationReason)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse{TResponse}"/> class.
        /// <br>Use when custom status <see cref="ResponseStatus"/></br>
        /// </summary>
        /// <param name="status">The stats</param>
        /// <param name="cancellationReason">The cancellation reason</param>
        public CommandResponse(ResponseStatus status, string cancellationReason)
            : base(status, cancellationReason)
        {
        }

        /// <summary>
        /// Gets or sets the result
        /// </summary>
        public TResponse? Result { get; set; }
    }
}
