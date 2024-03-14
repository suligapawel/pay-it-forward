using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.HelpAccounts.Core.Exceptions;

public class NotFound : AppException
{
    public override int Code { get; init; } = 404;

    public NotFound(Type type, Guid id) : base($"The {type.Name} with id {id} does not exist.")
    {
    }
}