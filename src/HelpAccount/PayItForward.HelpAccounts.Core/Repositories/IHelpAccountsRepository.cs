using PayItForward.HelpAccounts.Core.Entities;

namespace PayItForward.HelpAccounts.Core.Repositories;

public interface IHelpAccountsRepository
{
    Task<HelpAccount> Get(Guid accountOwnerId, CancellationToken cancellationToken);
    Task Insert(HelpAccount helpAccount, CancellationToken cancellationToken);
    Task Update(HelpAccount helpAccount, CancellationToken cancellationToken);
}