using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.AskingForHelps.Domain.Events;

public record InterestExpressed(AskingForHelpId AskingForHelpId, PotentialHelper PotentialHelper) : DomainEvent;