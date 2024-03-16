using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.Tests.Fixtures;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests;

public class CreateRequestForHelpTests
{
    private CreateRequestForHelpHandler _handler;
    private IRequestForHelpRepository _repo;
    private CancellationToken _cancellationToken;
    private FakeHelpAccountProxy _helpAccountProxy;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _repo = new FakeRequestForHelpRepository();
        _helpAccountProxy = new FakeHelpAccountProxy();
        _handler = new CreateRequestForHelpHandler(_repo, _helpAccountProxy);
    }

    [Test]
    public async Task Should_create_request_for_help()
    {
        var needy = new Needy(Guid.Parse("a0c37b93-c47f-49f7-ba27-905b6ccba4f7"));
        var command = new CreateRequestForHelp(needy);

        await _handler.Handle(command, CancellationToken.None);

        var createdRequestForHelp = await _repo.Get(new RequestForHelpId(command.AggregateId), _cancellationToken);
        Assert.That(createdRequestForHelp.Id, Is.EqualTo(new RequestForHelpId(command.AggregateId)));
    }

    [Test]
    public void Should_not_create_request_for_help_when_needy_has_debt()
    {
        var needy = new Needy(Guid.Parse("a0c37b93-c47f-49f7-ba27-905b6ccba4f7"));
        var command = new CreateRequestForHelp(needy);
        _helpAccountProxy.SetCanIncurDebt(false);

        Assert.ThrowsAsync<TheNeedyCannotIncurDebt>(() => _handler.Handle(command, CancellationToken.None));
    }
}