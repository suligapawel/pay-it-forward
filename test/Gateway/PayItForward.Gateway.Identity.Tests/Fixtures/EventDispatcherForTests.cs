using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Gateway.Identity.Tests.Fixtures;

public class EventDispatcherForTests : IEventDispatcher
{
    public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent => Task.CompletedTask;
}