using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class HasAlreadyExpressedInterest(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} already exists in potential helpers groups.");