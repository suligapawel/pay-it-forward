using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests;

public class CreateRequestForHelpTests
{
    private CreateRequestForHelpHandler _handler;
    private IRequestForHelpRepository _repo;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _repo = new FakeRequestForHelpRepository();
        _handler = new CreateRequestForHelpHandler(_repo);
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
}