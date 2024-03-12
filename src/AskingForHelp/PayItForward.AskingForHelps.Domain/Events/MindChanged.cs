using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.AskingForHelps.Domain.Events;

public record MindChanged(AskingForHelpId AskingForHelpId, PotentialHelper PotentialHelper) : DomainEvent;