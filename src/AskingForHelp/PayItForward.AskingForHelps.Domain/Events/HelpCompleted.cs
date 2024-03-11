using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Events;

public record HelpCompleted(ActiveHelpId ActiveHelpId, Helper Helper);