using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Implementations.CancellationTokens;

namespace PayItForward.Shared.CQRS.Commands;

internal class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public CommandDispatcher(IServiceProvider serviceProvider, ICancellationTokenProvider cancellationTokenProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _cancellationTokenProvider = cancellationTokenProvider ?? throw new ArgumentNullException(nameof(cancellationTokenProvider));
    }

    public async Task Execute<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var commandHandlerType = typeof(ICommandHandler<TCommand>);
        if (_serviceProvider.GetService(commandHandlerType) is not ICommandHandler<TCommand> commandHandler)
        {
            throw new NotSupportedException($"Command handler of type {commandHandlerType} has not been registered.");
        }

        await commandHandler.Handle(command, _cancellationTokenProvider.CreateToken());
    }
}