using System.Text;

namespace PayItForward.Gateway.Identity.Settings;

internal sealed class TokensSettings
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public TokenSettings Refresh { get; init; }
    public TokenSettings Access { get; init; }
}

internal sealed class TokenSettings
{
    public string Secret { get; init; }
    public double ExpirationTime { get; init; }

    public byte[] SecretAsBytes() => Encoding.UTF8.GetBytes(Secret);
}