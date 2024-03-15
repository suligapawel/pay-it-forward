using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.Helps.Domain.Events;

public record MindChanged(AskingForHelpId AskingForHelpId, PotentialHelper PotentialHelper) : DomainEvent;