using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class PotentialHelperAndNeedyAreTheSamePerson(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} is the needy.")
{
}