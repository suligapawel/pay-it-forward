using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.Gateway.Identity.Exceptions;

internal sealed class BadCredentialsException : AppException
{
    public BadCredentialsException() : base("Wrong email or password.")
    {
    }
}