namespace PayItForward.Gateway.Identity.Settings;

internal sealed class IdentitySettings
{
    public PasswordSettings Passwords { get; init; }
    public TokensSettings Tokens { get; init; }
}