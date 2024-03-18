using PayItForward.Helps.Domain.ValueObjects;
using CreateRequestForHelpCommand = PayItForward.Helps.Application.Commands.CreateRequestForHelp;

namespace PayItForward.Helps.Api.Requests;

internal sealed record CreateRequestForHelp(string Description) : IRequest
{
    public static CreateRequestForHelpCommand AsCommand(Guid userId) => new(new Needy(userId));

    public bool IsValid() => throw new NotImplementedException();
}