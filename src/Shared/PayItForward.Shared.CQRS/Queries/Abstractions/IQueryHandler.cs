namespace PayItForward.Shared.CQRS.Queries.Abstractions;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}