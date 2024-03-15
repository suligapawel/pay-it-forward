namespace PayItForward.HelpAccounts.Shared;

public interface IHelpAccountProxy
{
    Task IncurDebt(Guid accountOwnerId);
    Task PayOffDebt(Guid accountOwnerId);
    Task<bool> CanIncurDebt(Guid accountOwnerId);
}