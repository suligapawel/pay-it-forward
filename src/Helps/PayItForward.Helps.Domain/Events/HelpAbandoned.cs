using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Events;

namespace PayItForward.Helps.Domain.Events;

public record HelpAbandoned(ActiveHelpId HelpId, Helper Helper, DateTime At) : DomainEvent;