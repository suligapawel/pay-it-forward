namespace PayItForward.Shared.Implementations.CancellationTokens;

public interface ICancellationTokenProvider
{
    public CancellationToken CreateToken();
}