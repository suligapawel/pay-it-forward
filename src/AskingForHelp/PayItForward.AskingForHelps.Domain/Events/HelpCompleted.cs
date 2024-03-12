using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.AskingForHelps.Domain.Events;

public record HelpCompleted(ActiveHelpId ActiveHelpId, Helper Helper, DateTime At) : DomainEvent;