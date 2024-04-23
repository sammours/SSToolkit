namespace SSToolkit.Application.Commands.Core.Reference.Queries
{
    using SSToolkit.Application.Commands.Core.Queries;

    public class GetUserByIdQuery : QueryRequest<GetUserByIdQueryResponse>
    {
        public GetUserByIdQuery(int id)
        {
            this.UserId = id;
        }

        public int UserId { get; }
    }
}
