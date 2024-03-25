using PayItForward.Shared.Implementations.CancellationTokens;

namespace PayItForward.Shared.CQRS.Tests.Fixtures;

public class CancellationTokenProviderForTests : ICancellationTokenProvider
{
    public CancellationToken CreateToken() => CancellationToken.None;
}