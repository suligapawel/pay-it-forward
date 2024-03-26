using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record ExpressInterest(RequestForHelpId RequestForHelpId, PotentialHelper PotentialHelper) : Command;

internal sealed class ExpressInterestHandler : ICommandHandler<ExpressInterest>
{
    private readonly IRequestForHelpRepository _requestsForHelp;

    public ExpressInterestHandler(IRequestForHelpRepository requestsForHelp)
    {
        _requestsForHelp = requestsForHelp;
    }

    public async Task Handle(ExpressInterest command, CancellationToken cancellationToken)
    {
        var requestForHelp = await _requestsForHelp.Get(command.RequestForHelpId, cancellationToken);

        if (requestForHelp is null)
        {
            throw new RequestForHelpDoesNotExist(command.RequestForHelpId);
        }
        
        requestForHelp.ExpressInterest(command.PotentialHelper);

        await _requestsForHelp.Update(requestForHelp, cancellationToken);
    }
}