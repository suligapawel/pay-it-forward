using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests;

public class ExpressInterestHandlerTests
{
    private ExpressInterestHandler _handler;
    private IRequestForHelpRepository _repo;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _repo = new FakeRequestForHelpRepository();
        _handler = new ExpressInterestHandler(_repo);
    }

    [Test]
    public async Task Should_express_interest()
    {
        var requestForHelpId = FakeRequestForHelpRepository.ExistedRequestForHelpId;
        var potentialHelper = AnyPotentialHelper();
        var command = new ExpressInterest(requestForHelpId, potentialHelper);

        await _handler.Handle(command, _cancellationToken);

        var requestForHelp = await _repo.Get(requestForHelpId, _cancellationToken);
        Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.True);
    }

    private static PotentialHelper AnyPotentialHelper() => new(Guid.NewGuid());
}