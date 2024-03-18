using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Requests;
using CreateRequestForHelpCommand = PayItForward.Helps.Application.Commands.CreateRequestForHelp;

namespace PayItForward.Helps.Api.Requests;

internal sealed record CreateRequestForHelp(string Description) : IRequest
{
    public static CreateRequestForHelpCommand AsCommand(Guid userId) => new(new Needy(userId));

    public string Error { get; } = "Description is not valid";

    // TODO: valid obscene words
    public bool IsValid() => !string.IsNullOrWhiteSpace(Description);
}