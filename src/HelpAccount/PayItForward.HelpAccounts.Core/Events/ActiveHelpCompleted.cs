using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.HelpAccounts.Core.Events;

public record ActiveHelpCompleted(Guid HelperId) : IntegrationEvent;

public class ActiveHelpCompletedHandler : IEventHandler<ActiveHelpCompleted>
{
    private readonly IHelpAccountsRepository _helpAccounts;

    public ActiveHelpCompletedHandler(IHelpAccountsRepository helpAccounts)
    {
        _helpAccounts = helpAccounts;
    }

    public async Task Handle(ActiveHelpCompleted @event, CancellationToken cancellationToken)
    {
        var helpAccount = await _helpAccounts.Get(@event.HelperId, cancellationToken);

        if (helpAccount is null)
        {
            return;
        }

        helpAccount.PayOff();

        await _helpAccounts.Update(helpAccount, cancellationToken);
    }
}