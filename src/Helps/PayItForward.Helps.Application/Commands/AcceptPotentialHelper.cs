using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Helps.Application.Commands;

public record AcceptPotentialHelper(RequestForHelpId RequestForHelpId, Needy Needy, PotentialHelper PotentialHelper) : Command;

internal sealed class AcceptPotentialHelperHandler : ICommandHandler<AcceptPotentialHelper>
{
    private readonly IRequestForHelpRepository _requestsForHelp;
    private readonly IActiveHelpRepository _activeHelps;
    private readonly IClock _clock;

    public AcceptPotentialHelperHandler(
        IRequestForHelpRepository requestsForHelp,
        IActiveHelpRepository activeHelps,
        IClock clock)
    {
        _requestsForHelp = requestsForHelp;
        _activeHelps = activeHelps;
        _clock = clock;
    }

    public async Task Handle(AcceptPotentialHelper command, CancellationToken cancellationToken)
    {
        var requestForHelp = await _requestsForHelp.Get(command.RequestForHelpId, cancellationToken);

        if (requestForHelp is null)
        {
            throw new RequestForHelpDoesNotExist(command.RequestForHelpId);
        }

        var accepted = requestForHelp.Accept(command.Needy, command.PotentialHelper);

        // TODO: Think about expiryDate
        var activeHelp = ActiveHelp.NewFrom(accepted, _clock.Now.AddDays(7));
        
        await _requestsForHelp.Update(requestForHelp, cancellationToken);
        await _activeHelps.Add(activeHelp, cancellationToken);
    }
}