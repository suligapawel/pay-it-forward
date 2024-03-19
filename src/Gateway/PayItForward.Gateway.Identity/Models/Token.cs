namespace PayItForward.Gateway.Identity.Models;

internal sealed class Token
{
    public Guid Id { get; init; }
    public string Value { get; init; }
    public DateTime ExpiresIn { get; init; }
}

internal record OldToken(string Value);

internal record NewToken(string Value);
internal record Tokens(NewToken AccessToken, NewToken RefreshToken);