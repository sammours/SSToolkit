namespace SSToolkit.Application.Commands.Core.Reference.CustomBehaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SSToolkit.Application.Commands.Core.Behaviors;
    using SSToolkit.Application.Commands.Core.Queries;

    public class QueryOnlyBehavior<TRequest, TResponse> : BaseBehavior<TRequest, TResponse>
        where TRequest : class, IQueryRequest<TResponse>
    {
        public override async Task<TResponse> Process(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("Query only behavior");
            return await next().ConfigureAwait(false);
        }

        protected override bool CanProcess(TRequest request)
        {
            return true; // or conditional
        }
    }
}
