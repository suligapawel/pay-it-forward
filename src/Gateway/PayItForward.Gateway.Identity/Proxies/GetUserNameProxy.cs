using PayItForward.Gateway.Identity.Repositories;
using PayItForward.Gateway.Shared.Proxies;
using PayItForward.Shared.Implementations.CancellationTokens;

namespace PayItForward.Gateway.Identity.Proxies;

internal sealed class GetUserNameProxy : IGetUserNameProxy
{
    private readonly IUserRepository _users;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public GetUserNameProxy(IUserRepository users, ICancellationTokenProvider cancellationTokenProvider)
    {
        _users = users;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    public async Task<string> Get(Guid id)
    {
        var user = await _users.Get(id, _cancellationTokenProvider.CreateToken());
        return user.Name;
    }
}