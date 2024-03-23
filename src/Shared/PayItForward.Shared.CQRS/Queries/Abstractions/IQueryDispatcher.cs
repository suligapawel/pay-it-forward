namespace PayItForward.Shared.CQRS.Queries.Abstractions;

public interface IQueryDispatcher
{
    Task<TResult> Execute<TQuery, TResult>(TQuery query)
        where TQuery : IQuery;
}