using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Repositories;

public interface IRequestForHelpRepository
{
    Task<RequestForHelp> Get(RequestForHelpId id, CancellationToken cancellationToken);
    Task Create(RequestForHelp requestForHelp, CancellationToken cancellationToken);
    Task Update(RequestForHelp requestForHelp, CancellationToken cancellationToken);
    Task Delete(RequestForHelpId id, CancellationToken cancellationToken);
}