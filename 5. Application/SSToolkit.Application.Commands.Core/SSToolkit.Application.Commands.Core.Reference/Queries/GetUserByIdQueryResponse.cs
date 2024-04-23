namespace SSToolkit.Application.Commands.Core.Reference.Queries
{
    public class GetUserByIdQueryResponse
    {
        public GetUserByIdQueryResponse(User user)
        {
            this.User = user;
        }

        public User User { get; }
    }
}
