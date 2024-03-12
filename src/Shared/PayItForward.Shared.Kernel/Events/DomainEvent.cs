namespace PayItForward.Shared.Kernel.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
}