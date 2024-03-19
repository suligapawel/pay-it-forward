using PayItForward.Shared.Requests;

namespace PayItForward.Gateway.Identity.API.Requests;

// TODO: More advanced validator like more characters and so on
// TODO: Tests
internal record SignIn(string Email, string Password) : IRequest
{
    public string Error => "Wrong email or password.";

    public bool IsValid()
        => !string.IsNullOrWhiteSpace(Email)
           && !string.IsNullOrWhiteSpace(Password);
}