using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class SomeoneIsAlreadyHelpingWithThis()
    : DomainException("The needy has already chosen a person to help.")
{
}