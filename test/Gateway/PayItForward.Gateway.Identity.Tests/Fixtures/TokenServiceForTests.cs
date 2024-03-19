using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Services.Abstraction;

namespace PayItForward.Gateway.Identity.Tests.Fixtures;

internal sealed class TokenServiceForTests : ITokenService
{
    public const string FakeTokenValue = "Fake.token.value";

    public Token GenerateToken(TokenPayload tokenPayload, TokenType tokenType) => new() { Value = FakeTokenValue };

    public bool ValidToken(string token, TokenType tokenType) => true;

    public Guid GetUserId(string token) => Guid.NewGuid();
}