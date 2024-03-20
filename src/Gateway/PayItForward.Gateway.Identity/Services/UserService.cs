using System.Text;
using PayItForward.Gateway.Identity.Exceptions;
using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Repositories;
using PayItForward.Gateway.Identity.Services.Abstraction;

namespace PayItForward.Gateway.Identity.Services;

internal sealed class UserService : IUserService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public UserService(
        IUserRepository users,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<Guid> SignUp(string email, string password, CancellationToken cancellationToken)
    {
        if (await _users.Exists(email, cancellationToken))
        {
            throw new UserAlreadyExistsException();
        }

        var salt = _passwordHasher.GenerateSalt();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = _passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(password), salt),
            Salt = salt,
            EmailConfirmed = false,
        };

        await _users.Register(user, cancellationToken);
        // TODO: Publish event and consume it in help account project for create account 

        return user.Id;
    }

    public async Task<Tokens> SignIn(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _users.Get(email, cancellationToken);
        if (user is null)
        {
            throw new BadCredentialsException();
        }

        var passwordHash = _passwordHasher.ComputeHash(Encoding.UTF8.GetBytes(password), user.Salt);
        if (!passwordHash.SequenceEqual(user.Password))
        {
            throw new BadCredentialsException();
        }

        var refreshToken = _tokenService.GenerateToken(new TokenPayload(user), TokenType.Refresh);

        user.AddToken(refreshToken);
        await _users.AddToken(user.Id, refreshToken, cancellationToken);

        return CreateNewTokens(user, refreshToken);
    }

    // TODO: Add tests
    public async Task<Tokens> Refresh(OldToken oldRefreshToken, CancellationToken cancellationToken)
    {
        var isValid = _tokenService.ValidToken(oldRefreshToken.Value, TokenType.Refresh);
        if (!isValid)
        {
            throw new RefreshTokenIsNotValidException();
        }

        var userId = _tokenService.GetUserId(oldRefreshToken.Value);

        var user = await _users.Get(userId, cancellationToken);
        if (user is null)
        {
            throw new RefreshTokenIsNotValidException();
        }

        var hasToken = user.Tokens.Any(x => x.Value == oldRefreshToken.Value);

        if (!hasToken)
        {
            throw new RefreshTokenIsNotValidException();
        }

        var newRefreshToken = _tokenService.GenerateToken(new TokenPayload(user), TokenType.Refresh);

        // TODO: Change it
        user.RefreshToken(new Token { Value = oldRefreshToken.Value }, newRefreshToken);
        await _users.RefreshToken(user.Id, oldRefreshToken, newRefreshToken, cancellationToken);

        return CreateNewTokens(user, newRefreshToken);
    }

    private Tokens CreateNewTokens(User user, Token newRefreshToken)
        => new(
            new NewToken(_tokenService.GenerateToken(new TokenPayload(user), TokenType.Access).Value),
            new NewToken(newRefreshToken.Value));
}