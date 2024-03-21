using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Shared.CQRS.Tests.Fixtures.Events.DifferentNamespace;

public record EventForTest(Guid Id) : IEvent;