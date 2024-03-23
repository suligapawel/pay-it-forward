using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Tests.Fixtures;
using PayItForward.Shared.CQRS.Tests.Fixtures.Commands;

namespace PayItForward.Shared.CQRS.Tests;

public class CommandDispatcherTests
{
    private ICommandDispatcher _dispatcher;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs(GetType().Assembly);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        _dispatcher = serviceProvider.GetService<ICommandDispatcher>();
    }

    [Test]
    public void Should_execute_command_handler()
    {
        // It's weird but works
        Assert.ThrowsAsync<HandlerWasExecuted>(() => _dispatcher.Execute(new CommandWithHandler()));
    }
    
    [Test]
    public void Should_throw_not_supported_exception_when_command_does_not_have_handler()
    {
        Assert.ThrowsAsync<NotSupportedException>(() => _dispatcher.Execute(new CommandWithoutHandler()));
    }
}