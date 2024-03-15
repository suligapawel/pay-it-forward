using PayItForward.HelpAccounts.Core.Entities;
using PayItForward.HelpAccounts.Core.Exceptions;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Shared;

namespace PayItForward.HelpAccounts.Core.Services;

internal sealed class HelpAccountService : IHelpAccountService, IHelpAccountProxy
{
    private readonly IHelpAccountsRepository _helpAccounts;

    public HelpAccountService(IHelpAccountsRepository helpAccounts)
    {
        _helpAccounts = helpAccounts;
    }

    public async Task IncurDebt(Guid accountOwnerId)
    {
        var helpAccount = await _helpAccounts.Get(accountOwnerId);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        helpAccount.Incur();

        await _helpAccounts.Update(helpAccount);
    }

    public async Task PayOffDebt(Guid accountOwnerId)
    {
        var helpAccount = await _helpAccounts.Get(accountOwnerId);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        helpAccount.PayOff();

        await _helpAccounts.Update(helpAccount);
    }

    public async Task<bool> CanIncurDebt(Guid accountOwnerId)
    {
        var helpAccount = await _helpAccounts.Get(accountOwnerId);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        return helpAccount.CanIncurTheDebt();
    }
}