using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests;

public class DoNotHelpHandlerTests
{
    private DoNotHelpHandler _handler;
    private IRequestForHelpRepository _repo;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _repo = new FakeRequestForHelpRepository();
        _handler = new DoNotHelpHandler(_repo, new FakeRequestForHelpViewModelRepository());
    }

    [Test]
    public async Task Should_express_interest()
    {
        var requestForHelpId = FakeRequestForHelpRepository.ExistedRequestForHelpId;
        var requestForHelp = await _repo.Get(requestForHelpId, _cancellationToken);
        var potentialHelper = AnyPotentialHelper();
        requestForHelp.ExpressInterest(potentialHelper);
        var command = new DoNotHelp(requestForHelpId, potentialHelper);

        await _handler.Handle(command, _cancellationToken);

        Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
    }

    [Test]
    public void Should_not_express_interest_when_request_for_help_does_not_exist()
    {
        var command = new DoNotHelp(FakeRequestForHelpRepository.NotExistedRequestForHelpId, AnyPotentialHelper());

        Assert.ThrowsAsync<RequestForHelpDoesNotExist>(() => _handler.Handle(command, _cancellationToken));
    }

    private static PotentialHelper AnyPotentialHelper() => new(Guid.NewGuid());
}