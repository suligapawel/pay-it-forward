using PayItForward.Gateway.Identity.Models;

namespace PayItForward.Gateway.Identity.Services.Abstraction;

internal interface IUserService
{
    Task<Guid> SignUp(string email, string name, string password, CancellationToken cancellationToken);
    Task<Tokens> SignIn(string email, string password, CancellationToken cancellationToken);
    Task<Tokens> Refresh(OldToken oldRefreshToken, CancellationToken cancellationToken);
}