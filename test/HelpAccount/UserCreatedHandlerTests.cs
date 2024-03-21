using PayItForward.Debts.Core.Tests.Fixtures;
using PayItForward.HelpAccounts.Core.Events;

namespace PayItForward.Debts.Core.Tests;

public class UserCreatedHandlerTests
{
    private readonly UserCreatedHandler _handler;
    private readonly HelpAccountsForTestsRepository _repository;
    private readonly CancellationToken _cancellationToken;

    public UserCreatedHandlerTests()
    {
        _cancellationToken = CancellationToken.None;
        _repository = new HelpAccountsForTestsRepository();
        _handler = new UserCreatedHandler(_repository);
    }

    [Test]
    public async Task Should_create_help_account_when_user_was_created()
    {
        var @event = new UserCreated(Guid.NewGuid());

        await _handler.Handle(@event, _cancellationToken);

        var helpAccount = await _repository.Get(@event.UserId, _cancellationToken);
        Assert.That(helpAccount, Is.Not.Null);
        Assert.That(helpAccount.AccountOwner, Is.EqualTo(@event.UserId));
    }
}