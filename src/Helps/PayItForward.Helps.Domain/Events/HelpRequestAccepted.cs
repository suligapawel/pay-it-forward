using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Events;

public record HelpRequestAccepted(Needy Needy, PotentialHelper PotentialHelper);