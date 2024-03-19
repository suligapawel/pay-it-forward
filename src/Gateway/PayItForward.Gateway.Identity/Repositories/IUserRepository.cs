using PayItForward.Gateway.Identity.Models;

namespace PayItForward.Gateway.Identity.Repositories;

internal interface IUserRepository
{
    Task<bool> Exists(string email, CancellationToken cancellationToken);
    Task Register(User user, CancellationToken cancellationToken);
    Task<User> Get(string email, CancellationToken cancellationToken);
    Task<User> Get(Guid id, CancellationToken cancellationToken);
    
    Task AddToken(Guid userId, Token token, CancellationToken cancellationToken);
    Task RefreshToken(Guid userId, OldToken oldToken, Token newToken, CancellationToken cancellationToken);
}