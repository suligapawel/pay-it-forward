using System.Security.Claims;
using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity.Models;

internal class RefreshToken : IToken
{
    public Dictionary<string, object> CreatePayload(TokenPayload tokenPayload)
    {
        ArgumentNullException.ThrowIfNull(tokenPayload);
        ArgumentNullException.ThrowIfNull(tokenPayload.User);

        return new Dictionary<string, object>
        {
            { ClaimTypes.NameIdentifier, tokenPayload.User.Id }
        };
    }

    public TokenSettings GetSettings(TokensSettings settings) => settings?.Refresh;
}