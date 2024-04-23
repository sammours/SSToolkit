namespace SSToolkit.Application.Commands.Core.Behaviors
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    /// <summary>
    /// Base Behavior that implements the IPipeline Behavior from MediatR
    /// </summary>
    /// <remarks>
    /// This base behavior targets both commands and queries.
    /// However, they can be specified in the inheritance to target either the commands or the queries.
    /// (Make sure to cast the generic TRequest to IQueryRequest or ICommandRequest when needed)
    /// See reference project for examples
    /// </remarks>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class BaseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : class, IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        protected BaseBehavior()
        {
        }

        /// <summary>
        /// Handle method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public virtual async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!this.CanProcess(request))
            {
                return await next().ConfigureAwait(false);
            }

            return await this.Process(request, next, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Process method (Should be implemented)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<TResponse> Process(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);

        /// <summary>
        /// Write conditions when the pipeline behaviour should be activated (Should be implemented)
        /// </summary>
        /// <remarks>
        /// It can look for a specific parameter in the request or if the request implements a certain interface
        /// e.g.
        /// public class CustomQueryRequest : QueryRequest<int>, ILogging ...
        /// -----
        /// CanProcess
        /// return request is ILogging;
        /// -----
        /// In this case the logging should be applied always, so return true
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract bool CanProcess(TRequest request);
    }
}
