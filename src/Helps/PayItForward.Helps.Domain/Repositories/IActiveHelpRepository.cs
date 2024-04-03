using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Repositories;

public interface IActiveHelpRepository
{
    Task Add(ActiveHelp activeHelp, CancellationToken cancellationToken);
    Task<ActiveHelp> Get(ActiveHelpId id, CancellationToken cancellationToken);
}