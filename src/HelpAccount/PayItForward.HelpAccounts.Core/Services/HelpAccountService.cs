namespace PayItForward.HelpAccounts.Core.Services;

internal sealed class HelpAccountService : IHelpAccountService
{
    public Task IncurDebt(Guid accountOwnerId) => throw new NotImplementedException();

    public Task PayOffDebt(Guid accountOwnerId) => throw new NotImplementedException();

    public Task<bool> CanIncurDebt(Guid accountOwnerId) => throw new NotImplementedException();
}