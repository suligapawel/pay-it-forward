using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Helps.Application.Events;

public record ActiveHelpWasAbandoned(Guid ActiveHelpId, Guid AbandoningId) : IntegrationEvent;

public class ActiveHelpWasAbandonedHandler : IEventHandler<ActiveHelpWasAbandoned>
{
    public Task Handle(ActiveHelpWasAbandoned @event, CancellationToken cancellationToken) => throw new NotImplementedException();
}