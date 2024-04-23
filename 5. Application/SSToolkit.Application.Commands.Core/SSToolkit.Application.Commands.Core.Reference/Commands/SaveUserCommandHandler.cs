namespace SSToolkit.Application.Commands.Core.Reference.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using SSToolkit.Application.Commands.Core.Commands;

    public class SaveUserCommandHandler : CommandHandler<SaveUserCommand>
    {
        public SaveUserCommandHandler()
        {
        }

        public override async Task<CommandResponse> Process(SaveUserCommand request, CancellationToken cancellationToken)
        {
            // Save user
            return await Task.FromResult(new CommandResponse()).ConfigureAwait(false);
        }
    }
}
