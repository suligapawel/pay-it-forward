using PayItForward.HelpAccounts.Core.Entities;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.HelpAccounts.Core.Events;

public record UserCreated(Guid UserId) : IntegrationEvent;

public class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly IHelpAccountsRepository _helpAccounts;

    public UserCreatedHandler(IHelpAccountsRepository helpAccounts)
    {
        _helpAccounts = helpAccounts;
    }

    public async Task Handle(UserCreated @event, CancellationToken cancellationToken)
    {
        await _helpAccounts.Insert(new HelpAccount(@event.UserId, 0), cancellationToken);
    }
}