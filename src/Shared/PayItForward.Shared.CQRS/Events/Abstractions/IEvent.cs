namespace PayItForward.Shared.CQRS.Events.Abstractions;

public interface IEvent
{
    Guid Id { get; }
}