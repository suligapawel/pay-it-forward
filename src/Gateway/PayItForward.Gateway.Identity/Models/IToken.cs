using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity.Models;

internal interface IToken
{
    Dictionary<string, object> CreatePayload(TokenPayload payload);
    TokenSettings GetSettings(TokensSettings settings);
}