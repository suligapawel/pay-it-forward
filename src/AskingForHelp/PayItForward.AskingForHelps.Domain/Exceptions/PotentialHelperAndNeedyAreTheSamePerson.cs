using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.AskingForHelps.Domain.Exceptions;

public class PotentialHelperAndNeedyAreTheSamePerson(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} is the needy.")
{
}