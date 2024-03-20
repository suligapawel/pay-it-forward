using PayItForward.Gateway.Identity.Models;

namespace PayItForward.Gateway.Identity.Repositories;

internal sealed class InMemoryUserRepository : IUserRepository
{
    private static readonly Dictionary<Guid, User> Users = new();

    public Task<bool> Exists(string email, CancellationToken cancellationToken)
        => Task.FromResult(Users.Values.Any(x => x.Email == email));

    public Task Register(User user, CancellationToken cancellationToken)
    {
        Users.Add(user.Id, user);
        return Task.CompletedTask;
    }

    public Task<User> Get(string email, CancellationToken cancellationToken)
        => Task.FromResult(Users.Values.FirstOrDefault(x => x.Email == email));

    public Task<User> Get(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(Users.GetValueOrDefault(id));


    public Task AddToken(Guid userId, Token token, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task RefreshToken(Guid userId, OldToken oldToken, Token newToken, CancellationToken cancellationToken)
        => Task.CompletedTask;
}