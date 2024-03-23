using PayItForward.Shared.CQRS.Queries.Abstractions;

namespace PayItForward.Shared.CQRS.Tests.Fixtures.Queries;

public record QueryWithHandler : IQuery;

public class QueryWithHandlerHandler : IQueryHandler<QueryWithHandler, int>
{
    public Task<int> Handle(QueryWithHandler query, CancellationToken cancellationToken)
    {
        throw new HandlerWasExecuted();
    }
}
