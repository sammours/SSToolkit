namespace SSToolkit.Application.Commands.Core.Reference.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using SSToolkit.Application.Commands.Core.Commands;

    public class ConditionalCommandHandler : CommandHandler<ConditionalCommand>
    {
        public override async Task<CommandResponse> Process(ConditionalCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new CommandResponse()).ConfigureAwait(false);
        }
    }
}
