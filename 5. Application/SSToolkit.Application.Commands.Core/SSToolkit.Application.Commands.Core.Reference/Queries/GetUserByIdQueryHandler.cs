namespace SSToolkit.Application.Commands.Core.Reference.Queries
{
    using SSToolkit.Application.Commands.Core.Queries;

    public class GetUserByIdQueryHandler : QueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        public GetUserByIdQueryHandler()
        {
        }

        public override async Task<QueryResponse<GetUserByIdQueryResponse>> Process(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == 0)
            {
                throw new ArgumentNullException();
            }

            var user = new User()
            {
                Id = request.UserId,
                Name = $"User " + request.UserId
            };

            await Task.Delay(1).ConfigureAwait(false); // For the Task method
            return new QueryResponse<GetUserByIdQueryResponse>(new GetUserByIdQueryResponse(user));
        }
    }
}
