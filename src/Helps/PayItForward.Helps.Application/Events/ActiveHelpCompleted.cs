using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Helps.Application.Events;

public record ActiveHelpCompleted(Guid ActiveHelpId, Guid HelperId) : IntegrationEvent;