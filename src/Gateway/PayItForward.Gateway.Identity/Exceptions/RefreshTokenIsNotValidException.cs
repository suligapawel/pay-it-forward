using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.Gateway.Identity.Exceptions;

internal sealed class RefreshTokenIsNotValidException : AppException
{
    public RefreshTokenIsNotValidException() : base("Wrong token.")
    {
    }
}