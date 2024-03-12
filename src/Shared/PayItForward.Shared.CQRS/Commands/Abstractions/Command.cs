namespace PayItForward.Shared.CQRS.Commands.Abstractions;

public abstract record Command : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}