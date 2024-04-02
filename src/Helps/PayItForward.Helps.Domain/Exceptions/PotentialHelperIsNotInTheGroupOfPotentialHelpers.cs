using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class PotentialHelperIsNotInTheGroupOfPotentialHelpers(PotentialHelper potentialHelper)
: DomainException($"The potential helper with id {potentialHelper.Id} is not in the group of potential helpers.");