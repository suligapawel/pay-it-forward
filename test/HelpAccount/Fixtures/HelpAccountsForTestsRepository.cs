using PayItForward.HelpAccounts.Core.Entities;
using PayItForward.HelpAccounts.Core.Repositories;

namespace PayItForward.Debts.Core.Tests.Fixtures;

public class HelpAccountsForTestsRepository : IHelpAccountsRepository
{
    private static readonly Guid AccountOwnerId = Guid.Parse("566d3130-7986-4fcd-9fba-b675e38bf478");

    private readonly Dictionary<Guid, HelpAccount> _helpAccounts =
        new() { { AccountOwnerId, new HelpAccount(AccountOwnerId, 0) } };

    public Task<HelpAccount> Get(Guid accountOwnerId, CancellationToken cancellationToken)
        => Task.FromResult(_helpAccounts.GetValueOrDefault(accountOwnerId));

    public Task Insert(HelpAccount helpAccount, CancellationToken cancellationToken)
    {
        _helpAccounts.Add(helpAccount.AccountOwner, helpAccount);
        return Task.CompletedTask;
    }

    public Task Update(HelpAccount helpAccount, CancellationToken cancellationToken) => Task.CompletedTask;
}