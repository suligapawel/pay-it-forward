using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Infrastructure.Repositories;

public class InMemoryActiveHelpRepository : IActiveHelpRepository
{
    private static readonly Dictionary<ActiveHelpId, List<ActiveHelp>> ActiveHelps = new();

    public Task Add(ActiveHelp activeHelp, CancellationToken cancellationToken)
    {
        if (ActiveHelps.TryGetValue(activeHelp.Id, out var help))
        {
            help.Add(activeHelp);
        }
        else
        {
            ActiveHelps.Add(activeHelp.Id, [activeHelp]);
        }

        return Task.CompletedTask;
    }

    public Task Update(ActiveHelp activeHelp, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task<ActiveHelp> Get(ActiveHelpId id, CancellationToken cancellationToken)
        => Task.FromResult(ActiveHelps.GetValueOrDefault(id).FirstOrDefault(x => x.IsActive()));
}