namespace PayItForward.Shared.CQRS.Commands.Abstractions;

public interface ICommandDispatcher
{
    Task Execute<TCommand>(TCommand command)
        where TCommand : ICommand;
}