namespace PayItForward.Shared.CQRS.Events.Abstractions;

public abstract record IntegrationEvent : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
}