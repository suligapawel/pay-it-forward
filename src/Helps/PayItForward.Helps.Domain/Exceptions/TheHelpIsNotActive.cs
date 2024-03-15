using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class TheHelpIsNotActive(ActiveHelpId helpId)
    : DomainException($"The active help with id {helpId.Value} is completed or approved.");