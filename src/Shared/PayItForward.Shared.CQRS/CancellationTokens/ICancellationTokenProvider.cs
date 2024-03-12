namespace PayItForward.Shared.CQRS.CancellationTokens;

public interface ICancellationTokenProvider
{
    public CancellationToken CreateToken();
}