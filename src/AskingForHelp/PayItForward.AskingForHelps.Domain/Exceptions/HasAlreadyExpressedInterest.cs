using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Exceptions;

public class HasAlreadyExpressedInterest(PotentialHelper potentialHelper)
    : DomainException($"The potential helper with id {potentialHelper.Id} already exists in potential helpers groups.");