using PayItForward.Shared.CQRS.CancellationTokens;

namespace PayItForward.Debts.Core.Tests.Fixtures;

public class CancellationTokenProviderForTests : ICancellationTokenProvider
{
    public CancellationToken CreateToken() => CancellationToken.None;
}