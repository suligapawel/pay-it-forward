namespace PayItForward.Shared.Kernel.Exceptions;

public abstract class DomainException(string message) : Exception(message);