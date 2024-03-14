namespace PayItForward.Shared.Implementations.Exceptions;

public abstract class AppException : Exception
{
    public virtual int Code { get; init; } = 400;

    protected AppException(string message) : base(message)
    {
    }
}