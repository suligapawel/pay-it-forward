using System.Security.Claims;
using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity.Models;

internal class AccessToken : IToken
{
    public Dictionary<string, object> CreatePayload(TokenPayload payload)
    {
        return new Dictionary<string, object>
        {
            { ClaimTypes.NameIdentifier, payload.User.Id.ToString() },
            { ClaimTypes.Email, payload.User.Email },
            { ClaimTypes.Name, payload.User.Name },
            { ClaimTypes.Role, payload.User.Roles }
        };
    }

    public TokenSettings GetSettings(TokensSettings settings) => settings?.Access;
}