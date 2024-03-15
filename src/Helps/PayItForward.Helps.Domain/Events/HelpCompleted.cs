using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.Helps.Domain.Events;

public record HelpCompleted(ActiveHelpId ActiveHelpId, Helper Helper, DateTime At) : DomainEvent;