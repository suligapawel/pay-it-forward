using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Requests;
using ExpressInterestCommand = PayItForward.Helps.Application.Commands.ExpressInterest;


namespace PayItForward.Helps.Api.Requests;

public record ExpressInterest : IRequest
{
    public string Error => string.Empty;
    public bool IsValid() => true;

    public ExpressInterestCommand AsCommand(Guid requestForHelpId, Guid helper)
        => new(new RequestForHelpId(requestForHelpId), new Helper(helper));
}