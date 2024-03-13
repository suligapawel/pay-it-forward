namespace PayItForward.HelpAccounts.Core.Services;

public interface IHelpAccountService
{
    Task IncurDebt(Guid accountOwnerId);
    Task PayOffDebt(Guid accountOwnerId);
    Task<bool> CanIncurDebt(Guid accountOwnerId);
}