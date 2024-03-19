using Microsoft.Extensions.Options;
using PayItForward.Gateway.Identity.Exceptions;
using PayItForward.Gateway.Identity.Repositories;
using PayItForward.Gateway.Identity.Services;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Gateway.Identity.Settings;
using PayItForward.Gateway.Identity.Tests.Fixtures;

namespace PayItForward.Gateway.Identity.Tests;

internal sealed class SignUpTests
{
    private readonly IUserRepository _users;
    private readonly IUserService _service;
    private readonly CancellationToken _cancellationToken;

    public SignUpTests()
    {
        var tokenService = new TokenServiceForTests();
        var passwordHasher = new PasswordHasher(Options.Create(new PasswordSettings()));

        _users = new UsersForTestsRepository();
        _cancellationToken = CancellationToken.None;
        _service = new UserService(_users, passwordHasher, tokenService);
    }

    [Test]
    public async Task Should_register_user()
    {
        const string email = "something@test.com";

        await _service.SignUp(email, "fake_password11", _cancellationToken);

        var user = await _users.Get(email, _cancellationToken);
        Assert.That(user.Email, Is.EqualTo(email));
    }

    [Test]
    public async Task Should_throw_userAlreadyExistsException_when_given_email_is_busy()
    {
        const string email = "something_1@test.com";
        const string password = "fake_password11";

        await _service.SignUp(email, password, _cancellationToken);

        Assert.ThrowsAsync<UserAlreadyExistsException>(() => _service.SignUp(email, password, _cancellationToken));
    }
}