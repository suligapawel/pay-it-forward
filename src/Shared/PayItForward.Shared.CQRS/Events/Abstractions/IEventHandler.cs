namespace PayItForward.Shared.CQRS.Events.Abstractions;

public interface IEventHandler<in TEvent>
    where TEvent : IEvent
{
    Task Handle(TEvent @event, CancellationToken cancellationToken);
}