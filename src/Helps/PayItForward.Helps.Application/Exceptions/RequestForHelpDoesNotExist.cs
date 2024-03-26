using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Implementations.Exceptions;

namespace PayItForward.Helps.Application.Exceptions;

public class RequestForHelpDoesNotExist : AppException
{
    public RequestForHelpDoesNotExist(RequestForHelpId id)
        : base($"The request for help with id {id.Value} does not exist.")
    {
    }
}