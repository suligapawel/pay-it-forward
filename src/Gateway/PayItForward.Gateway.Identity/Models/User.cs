namespace PayItForward.Gateway.Identity.Models;

internal sealed class User
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public byte[] Password { get; init; }
    public byte[] Salt { get; init; }
    public string Email { get; init; }
    public bool EmailConfirmed { get; init; }
    public List<Role> Roles { get; init; } = [];
    public List<Token> Tokens { get; init; } = [];

    public void AddToken(Token token) => Tokens.Add(token);
    public void RefreshToken(Token oldToken, Token newToken)
    {
        Tokens.Remove(oldToken);
        Tokens.Add(newToken);
    }
}