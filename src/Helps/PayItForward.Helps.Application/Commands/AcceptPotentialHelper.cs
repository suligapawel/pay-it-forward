using PayItForward.HelpAccounts.Shared;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record AcceptPotentialHelper(RequestForHelpId RequestForHelpId, Needy Needy, PotentialHelper PotentialHelper) : Command;

internal sealed class AcceptPotentialHelperHandler : ICommandHandler<AcceptPotentialHelper>
{
    private readonly IRequestForHelpRepository _requestsForHelp;

    public AcceptPotentialHelperHandler(IRequestForHelpRepository requestsForHelp)
    {
        _requestsForHelp = requestsForHelp;
    }

    public async Task Handle(AcceptPotentialHelper command, CancellationToken cancellationToken)
    {
        var requestForHelp = await _requestsForHelp.Get(command.RequestForHelpId, cancellationToken);

        if (requestForHelp is null)
        {
            throw new RequestForHelpDoesNotExist(command.RequestForHelpId);
        }

        requestForHelp.Accept(command.Needy, command.PotentialHelper);

        await _requestsForHelp.Update(requestForHelp, cancellationToken);
    }
}