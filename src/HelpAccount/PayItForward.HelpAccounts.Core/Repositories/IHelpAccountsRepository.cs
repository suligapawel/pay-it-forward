using PayItForward.HelpAccounts.Core.Entities;

namespace PayItForward.HelpAccounts.Core.Repositories;

public interface IHelpAccountsRepository
{
    Task<HelpAccount> Get(Guid accountOwnerId);
    Task Insert(HelpAccount helpAccount);
    Task Update(HelpAccount helpAccount);
}