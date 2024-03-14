using PayItForward.HelpAccounts.Core.Repositories;

namespace PayItForward.HelpAccounts.Core.Services;

internal sealed class HelpAccountService : IHelpAccountService
{
    private readonly IHelpAccountsRepository _helpAccounts;

    public HelpAccountService(IHelpAccountsRepository helpAccounts)
    {
        _helpAccounts = helpAccounts;
    }

    public async Task IncurDebt(Guid accountOwnerId)
    {
        var helpAccount = await _helpAccounts.Get(accountOwnerId);

        helpAccount.Incur();

        await _helpAccounts.Update(helpAccount);
    }

    public Task PayOffDebt(Guid accountOwnerId)
    {
        return Task.CompletedTask;
    }

    public Task<bool> CanIncurDebt(Guid accountOwnerId)
        => Task.FromResult(true);
}