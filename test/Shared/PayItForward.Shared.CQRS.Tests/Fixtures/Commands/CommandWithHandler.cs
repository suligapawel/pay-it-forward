using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Shared.CQRS.Tests.Fixtures.Commands;

public record CommandWithHandler : Command;

public class CommandWithHandlerHandler : ICommandHandler<CommandWithHandler>
{
    public Task Handle(CommandWithHandler command, CancellationToken cancellationToken)
    {
        throw new CommandHandlerWasExecuted();
    }
}
