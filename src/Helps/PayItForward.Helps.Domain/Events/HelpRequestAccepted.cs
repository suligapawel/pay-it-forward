using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Events;

public record HelpRequestAccepted(RequestForHelpId RequestForHelpId, Needy Needy, PotentialHelper PotentialHelper);