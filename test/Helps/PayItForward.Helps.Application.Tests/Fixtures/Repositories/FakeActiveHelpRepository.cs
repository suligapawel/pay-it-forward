using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests.Fixtures.Repositories;

public class FakeActiveHelpRepository : IActiveHelpRepository
{
    private readonly Dictionary<ActiveHelpId, ActiveHelp> _activeHelps = new();

    public Task Add(ActiveHelp activeHelp, CancellationToken cancellationToken)
    {
        _activeHelps.Add(activeHelp.Id, activeHelp);
        return Task.CompletedTask;
    }

    public Task<ActiveHelp> Get(ActiveHelpId id, CancellationToken cancellationToken)
        => Task.FromResult(_activeHelps.GetValueOrDefault(id));
}