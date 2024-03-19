using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.Gateway.Identity.Exceptions;

internal sealed class UserAlreadyExistsException : AppException
{
    public UserAlreadyExistsException() : base("User already exists.")
    {
    }
}