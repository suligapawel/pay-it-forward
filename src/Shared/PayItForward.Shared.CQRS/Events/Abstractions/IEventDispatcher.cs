namespace PayItForward.Shared.CQRS.Events.Abstractions;

public interface IEventDispatcher
{
    Task Publish<TEvent>(TEvent @event)
        where TEvent : IEvent;
}