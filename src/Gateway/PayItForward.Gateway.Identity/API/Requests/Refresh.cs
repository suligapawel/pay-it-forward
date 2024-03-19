using PayItForward.Shared.Requests;

namespace PayItForward.Gateway.Identity.API.Requests;

// TODO: More advanced validator
// TODO: Tests
internal record Refresh(string Token) : IRequest
{
    public string Error => "Wrong email or password.";

    public bool IsValid() => !string.IsNullOrWhiteSpace(Token);
}