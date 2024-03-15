using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.Helps.Domain.Events;

public record MindChanged(RequestForHelpId RequestForHelpId, PotentialHelper PotentialHelper) : DomainEvent;