using System.Text;
using Microsoft.Extensions.Options;
using PayItForward.Gateway.Identity.Exceptions;
using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Repositories;
using PayItForward.Gateway.Identity.Services;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Gateway.Identity.Settings;
using PayItForward.Gateway.Identity.Tests.Fixtures;

namespace PayItForward.Gateway.Identity.Tests;

internal sealed class SignInTests
{
    private IUserRepository _users;
    private IUserService _service;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void SetUp()
    {
        var tokenService = new TokenServiceForTests();
        var passwordHasher = new PasswordHasher(Options.Create(new PasswordSettings()));

        _users = new UsersForTestsRepository();
        _cancellationToken = CancellationToken.None;
        _service = new UserService(_users, passwordHasher, tokenService, new EventDispatcherForTests());
    }

    [Test]
    public async Task Should_create_refresh_token_for_user()
    {
        const string email = "something@test.com";
        const string password = "fake_password11";
        await _service.SignUp(email, "name", password, _cancellationToken);

        await _service.SignIn(email, password, _cancellationToken);

        var user = await _users.Get(email, _cancellationToken);
        Assert.That(user.Tokens.First().Value, Is.EqualTo(TokenServiceForTests.FakeTokenValue));
    }

    [Test]
    public void Should_throw_badCredentialsException_when_user_does_not_exist()
    {
        const string email = "something@test.com";

        Assert.ThrowsAsync<BadCredentialsException>(() => _service.SignIn(email, "fake_password11", _cancellationToken));
    }

    [Test]
    public async Task Should_throw_badCredentialsException_when_password_is_wrong()
    {
        const string email = "something@test.com";
        await _service.SignUp(email, "name","fake_password11", _cancellationToken);

        Assert.ThrowsAsync<BadCredentialsException>(() => _service.SignIn(email, "!!fake_password", _cancellationToken));
    }

    [Test]
    public async Task Should_throw_badCredentialsException_when_salt_is_wrong()
    {
        const string email = "something@test.com";
        const string password = "fake_password11";
        await _service.SignUp(email, "name",password, _cancellationToken);
        var user = await _users.Get(email, _cancellationToken);
        typeof(User)
            .GetProperty(nameof(User.Salt))
            ?.SetValue(user, Encoding.UTF8.GetBytes("Wrong_salt"));

        Assert.ThrowsAsync<BadCredentialsException>(() => _service.SignIn(email, password, _cancellationToken));
    }
}