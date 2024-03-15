using PayItForward.Shared.Kernel.Exceptions;

namespace PayItForward.Helps.Domain.Exceptions;

public class TimeIsUp(Guid id)
    : DomainException($"Time is up for {id}.");