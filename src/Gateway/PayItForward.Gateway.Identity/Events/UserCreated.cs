using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Gateway.Identity.Events;

public record UserCreated(Guid UserId) : IntegrationEvent;