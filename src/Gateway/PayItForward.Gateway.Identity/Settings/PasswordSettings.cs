namespace PayItForward.Gateway.Identity.Settings;

internal sealed class PasswordSettings
{
    public string Pepper { get; init; } = "34885b5b-3efd-4879-bc62-665a5f045dbd";
    public int SaltLength { get; init; } = 64;
    public int Iterations { get; init; } = 3;
}