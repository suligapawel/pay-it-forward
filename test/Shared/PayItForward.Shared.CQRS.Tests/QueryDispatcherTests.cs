using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.CQRS.CancellationTokens;
using PayItForward.Shared.CQRS.Queries;
using PayItForward.Shared.CQRS.Queries.Abstractions;
using PayItForward.Shared.CQRS.Tests.Fixtures;
using PayItForward.Shared.CQRS.Tests.Fixtures.Commands;
using PayItForward.Shared.CQRS.Tests.Fixtures.Queries;

namespace PayItForward.Shared.CQRS.Tests;

public class QueryDispatcherTests
{
    private IQueryDispatcher _dispatcher;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCqrs(GetType().Assembly);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        _dispatcher = serviceProvider.GetService<IQueryDispatcher>();
    }

    [Test]
    public void Should_execute_query_handler()
    {
        // It's weird but works
        Assert.ThrowsAsync<HandlerWasExecuted>(() => _dispatcher.Execute<QueryWithHandler, int>(new QueryWithHandler()));
    }

    [Test]
    public void Should_throw_when_query_handler_does_not_exists()
    {
        Assert.ThrowsAsync<NotSupportedException>(() => _dispatcher.Execute<QueryWithoutHandler, int>(new QueryWithoutHandler()));
    }

    [Test]
    public void Should_throw_when_query_is_null()
    {
        QueryWithHandler query = null;

        Assert.ThrowsAsync<ArgumentNullException>(() => _dispatcher.Execute<QueryWithHandler, int>(query));
    }
}