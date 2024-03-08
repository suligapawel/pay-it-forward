using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.AskingForHelps.Domain.Exceptions;

public class PotentialHelperAbandonedTheRequest(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} abandoned the request.")
{
}