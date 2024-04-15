using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Infrastructure.Repositories;

public class InMemoryActiveHelpRepository : IActiveHelpRepository
{
    private static readonly Dictionary<ActiveHelpId, ActiveHelp> ActiveHelps = new();

    public Task Add(ActiveHelp activeHelp, CancellationToken cancellationToken)
    {
        ActiveHelps.Add(activeHelp.Id, activeHelp);
        return Task.CompletedTask;
    }

    public Task Update(ActiveHelp activeHelp, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    public Task<ActiveHelp> Get(ActiveHelpId id, CancellationToken cancellationToken)
        => Task.FromResult(ActiveHelps.GetValueOrDefault(id));
}