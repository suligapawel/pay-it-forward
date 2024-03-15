using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class PotentialHelperAbandonedTheRequest(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} abandoned the request.")
{
}