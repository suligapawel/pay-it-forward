namespace PayItForward.Shared.CQRS.Commands.Abstractions;

public interface ICommand
{
    Guid Id { get; }
}