namespace SSToolkit.Application.Commands.Core.Reference.Commands
{
    using SSToolkit.Application.Commands.Core.Commands;

    public class SaveUserCommand : CommandRequest
    {
        public SaveUserCommand(User user)
        {
            this.User = user;
        }

        public User User { get; }
    }
}
