using PayItForward.Gateway.Identity.Models;

namespace PayItForward.Gateway.Identity.Repositories;

internal sealed class UserRepository : IUserRepository
{
    public Task<bool> Exists(string email, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task Register(User user, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<User> Get(string email, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<User> Get(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task AddToken(Guid userId, Token token, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task RefreshToken(Guid userId, OldToken oldToken, Token newToken, CancellationToken cancellationToken) => throw new NotImplementedException();
}