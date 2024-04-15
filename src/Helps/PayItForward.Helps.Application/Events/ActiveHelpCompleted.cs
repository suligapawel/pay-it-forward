using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Helps.Application.Events;

public record ActiveHelpCompleted(Guid ActiveHelpId, Guid HelperId) : IntegrationEvent;