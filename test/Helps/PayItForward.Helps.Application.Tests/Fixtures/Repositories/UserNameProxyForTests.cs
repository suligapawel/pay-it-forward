using PayItForward.Gateway.Shared.Proxies;

namespace PayItForward.Helps.Application.Tests.Fixtures.Repositories;

public class UserNameProxyForTests : IGetUserNameProxy
{
    public Task<string> Get(Guid id) => Task.FromResult("Michael Jordinho");
}