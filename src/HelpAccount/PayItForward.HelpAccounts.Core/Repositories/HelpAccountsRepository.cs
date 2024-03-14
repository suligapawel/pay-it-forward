using PayItForward.HelpAccounts.Core.Entities;

namespace PayItForward.HelpAccounts.Core.Repositories;

public class HelpAccountsRepository : IHelpAccountsRepository
{
    private static readonly Guid AccountOwnerId = Guid.Parse("566d3130-7986-4fcd-9fba-b675e38bf478");

    private static readonly Dictionary<Guid, HelpAccount> HelpAccounts =
        new() { { AccountOwnerId, new HelpAccount(AccountOwnerId, 0) } };

    public Task<HelpAccount> Get(Guid id) => Task.FromResult(HelpAccounts[AccountOwnerId]);

    public Task Insert(HelpAccount helpAccount)
    {
        HelpAccounts.Add(helpAccount.AccountOwner, helpAccount);
        return Task.CompletedTask;
    }

    public Task Update(HelpAccount helpAccount) => Task.CompletedTask;
}