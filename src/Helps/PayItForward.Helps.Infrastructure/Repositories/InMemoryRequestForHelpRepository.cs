using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Infrastructure.Repositories;

internal sealed class InMemoryRequestForHelpRepository : IRequestForHelpRepository
{
    private static readonly Dictionary<RequestForHelpId, RequestForHelp> RequestsForHelp = new();

    public Task<RequestForHelp> Get(RequestForHelpId id, CancellationToken cancellationToken)
        => Task.FromResult(RequestsForHelp.GetValueOrDefault(id));

    public Task Create(RequestForHelp requestForHelp, CancellationToken cancellationToken)
    {
        RequestsForHelp.Add(requestForHelp.Id, requestForHelp);
        return Task.CompletedTask;
    }

    public Task Update(RequestForHelp requestForHelp, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task Delete(RequestForHelpId id, CancellationToken cancellationToken)
    {
        RequestsForHelp.Remove(id);
        return Task.CompletedTask;
    }
}