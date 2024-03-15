using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class TheLeaverDoesNotBelongToTheGroupOfPotentialHelpers(PotentialHelper potentialHelper)
    : DomainException($"Leaver with id {potentialHelper.Id} does not belong to the group of potential helpers.")
{
}