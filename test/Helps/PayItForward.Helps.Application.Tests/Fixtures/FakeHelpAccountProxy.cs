using PayItForward.HelpAccounts.Shared;

namespace PayItForward.Helps.Application.Tests.Fixtures;

public class FakeHelpAccountProxy : IHelpAccountProxy
{
    private bool _canIncurDebt = true;

    public Task IncurDebt(Guid accountOwnerId) => Task.CompletedTask;

    public Task PayOffDebt(Guid accountOwnerId) => throw new NotImplementedException();

    public Task<bool> CanIncurDebt(Guid accountOwnerId) => Task.FromResult(_canIncurDebt);

    public void SetCanIncurDebt(bool value) => _canIncurDebt = value;
}