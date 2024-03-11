using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.AskingForHelps.Domain.Exceptions;

public class TheHelpIsNotActive(ActiveHelpId helpId)
    : DomainException($"The active help with id {helpId.Value} is completed or approved.");