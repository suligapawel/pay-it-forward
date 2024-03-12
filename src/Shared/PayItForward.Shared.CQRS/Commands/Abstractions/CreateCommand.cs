namespace PayItForward.Shared.CQRS.Commands.Abstractions;

public abstract record CreateCommand : Command
{
    public Guid AggregateId { get; } = Guid.NewGuid();
}