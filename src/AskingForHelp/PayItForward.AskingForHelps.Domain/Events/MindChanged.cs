using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Events;

public record MindChanged(AskingForHelpId AskingForHelpId, PotentialHelper PotentialHelper);