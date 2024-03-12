using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.AskingForHelps.Domain.Events;

public record HelpAbandoned(ActiveHelpId HelpId, Helper Helper, DateTime At) : DomainEvent;