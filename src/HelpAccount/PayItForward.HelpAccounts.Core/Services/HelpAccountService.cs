using PayItForward.HelpAccounts.Core.Entities;
using PayItForward.HelpAccounts.Core.Exceptions;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Shared;
using PayItForward.Shared.CQRS.CancellationTokens;

namespace PayItForward.HelpAccounts.Core.Services;

internal sealed class HelpAccountService : IHelpAccountService, IHelpAccountProxy
{
    private readonly IHelpAccountsRepository _helpAccounts;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public HelpAccountService(IHelpAccountsRepository helpAccounts, ICancellationTokenProvider cancellationTokenProvider)
    {
        _helpAccounts = helpAccounts;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    public async Task IncurDebt(Guid accountOwnerId)
    {
        var cancellationToken = _cancellationTokenProvider.CreateToken();
        var helpAccount = await _helpAccounts.Get(accountOwnerId, cancellationToken);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        helpAccount.Incur();

        await _helpAccounts.Update(helpAccount, cancellationToken);
    }

    public async Task PayOffDebt(Guid accountOwnerId)
    {
        var cancellationToken = _cancellationTokenProvider.CreateToken();

        var helpAccount = await _helpAccounts.Get(accountOwnerId, cancellationToken);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        helpAccount.PayOff();

        await _helpAccounts.Update(helpAccount, cancellationToken);
    }

    public async Task<bool> CanIncurDebt(Guid accountOwnerId)
    {
        var cancellationToken = _cancellationTokenProvider.CreateToken();

        var helpAccount = await _helpAccounts.Get(accountOwnerId, cancellationToken);
        if (helpAccount is null)
        {
            throw new NotFound(typeof(HelpAccount), accountOwnerId);
        }

        return helpAccount.CanIncurTheDebt();
    }
}