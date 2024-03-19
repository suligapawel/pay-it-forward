using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity.Services;

internal sealed class TokenFactory
{
    private readonly IToken _token;

    // TODO: Tests
    public TokenFactory(TokenType tokenType)
    {
        _token = tokenType switch
        {
            TokenType.Access => new AccessToken(),
            TokenType.Refresh => new RefreshToken(),
            _ => throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null)
        };
    }

    public TokenSettings GetSettings(TokensSettings settings) => _token.GetSettings(settings);

    public Dictionary<string, object> CreatePayload(TokenPayload payload) => _token.CreatePayload(payload);
}