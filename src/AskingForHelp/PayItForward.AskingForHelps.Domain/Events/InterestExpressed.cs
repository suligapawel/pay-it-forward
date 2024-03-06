using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Events;

public record InterestExpressed(AskingForHelpId AskingForHelpId, PotentialHelper PotentialHelper);