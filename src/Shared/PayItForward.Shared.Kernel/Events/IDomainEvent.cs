namespace PayItForward.Shared.Kernel.Events;

public interface IDomainEvent
{
    Guid Id { get; }
}