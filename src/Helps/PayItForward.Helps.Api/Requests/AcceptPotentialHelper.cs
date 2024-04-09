using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Requests;
using AcceptPotentialHelperCommand = PayItForward.Helps.Application.Commands.AcceptPotentialHelper;

namespace PayItForward.Helps.Api.Requests;

public record AcceptPotentialHelper(Guid PotentialHelperId) : IRequest
{
    public AcceptPotentialHelperCommand AsCommand(Guid requestForHelpId, Guid userId)
        => new(new RequestForHelpId(requestForHelpId), new Needy(userId), new PotentialHelper(PotentialHelperId));

    public string Error => "The request id is not valid";

    public bool IsValid() => PotentialHelperId != default;
}