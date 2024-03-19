using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Repositories;

namespace PayItForward.Gateway.Identity.Tests.Fixtures;

internal sealed class UsersForTestsRepository : IUserRepository
{
    private readonly Dictionary<string, User> _users = new();

    public Task<bool> Exists(string email, CancellationToken cancellationToken)
        => Task.FromResult(_users.ContainsKey(email));

    public Task Register(User user, CancellationToken cancellationToken)
    {
        _users.TryAdd(user.Email, user);
        return Task.CompletedTask;
    }

    public Task<User> Get(string email, CancellationToken cancellationToken)
        => Task.FromResult(_users.GetValueOrDefault(email));

    public Task<User> Get(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(_users.First(x => x.Value.Id == id).Value);

    public async Task AddToken(Guid userId, Token token, CancellationToken cancellationToken)
    {
        var user = await Get(userId, cancellationToken);

        user.AddToken(token);
    }

    public async Task RefreshToken(Guid userId, OldToken oldToken, Token newToken, CancellationToken cancellationToken)
    {
        var user = await Get(userId, cancellationToken);
        var token = user.Tokens.First(x => x.Value == oldToken.Value);

        user.RefreshToken(token, newToken);
    }
}