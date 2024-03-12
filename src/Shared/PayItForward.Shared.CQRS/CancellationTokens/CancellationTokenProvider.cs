using Microsoft.AspNetCore.Http;

namespace PayItForward.Shared.CQRS.CancellationTokens;

public class CancellationTokenProvider : ICancellationTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CancellationToken CreateToken()
        => _httpContextAccessor.HttpContext?.RequestAborted ?? new CancellationToken(true);
}