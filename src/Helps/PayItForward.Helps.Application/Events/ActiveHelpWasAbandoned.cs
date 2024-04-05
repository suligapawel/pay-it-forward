using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Helps.Application.Events;

public record ActiveHelpWasAbandoned(Guid ActiveHelpId, Guid AbandoningId) : IntegrationEvent;

public class ActiveHelpWasAbandonedHandler : IEventHandler<ActiveHelpWasAbandoned>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ActiveHelpWasAbandonedHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public Task Handle(ActiveHelpWasAbandoned @event, CancellationToken cancellationToken)
        => _commandDispatcher.Execute(new DoNotHelp(new RequestForHelpId(@event.ActiveHelpId), new PotentialHelper(@event.AbandoningId)));
}