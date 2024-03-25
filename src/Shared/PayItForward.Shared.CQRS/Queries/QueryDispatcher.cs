using PayItForward.Shared.CQRS.Queries.Abstractions;
using PayItForward.Shared.Implementations.CancellationTokens;

namespace PayItForward.Shared.CQRS.Queries;


internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public QueryDispatcher(IServiceProvider serviceProvider, ICancellationTokenProvider cancellationTokenProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _cancellationTokenProvider = cancellationTokenProvider ?? throw new ArgumentNullException(nameof(cancellationTokenProvider));
    }

    public async Task<TResult> Execute<TQuery, TResult>(TQuery query)
        where TQuery : IQuery
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryHandlerType = typeof(IQueryHandler<TQuery, TResult>);
        if (_serviceProvider.GetService(queryHandlerType) is not IQueryHandler<TQuery, TResult> queryHandler)
        {
            throw new NotSupportedException($"Query handler of type {queryHandlerType} has not been registered.");
        }

        return await queryHandler.Handle(query, _cancellationTokenProvider.CreateToken());
    }
}