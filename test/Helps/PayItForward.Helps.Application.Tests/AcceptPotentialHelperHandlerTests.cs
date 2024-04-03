using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Implementations;

namespace PayItForward.Helps.Application.Tests;

public class AcceptPotentialHelperHandlerTests
{
    private AcceptPotentialHelperHandler _handler;
    private IRequestForHelpRepository _requestsForHelpRepo;
    private IActiveHelpRepository _activeHelpRepo;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _requestsForHelpRepo = new FakeRequestForHelpRepository();
        _activeHelpRepo = new FakeActiveHelpRepository();
        _handler = new AcceptPotentialHelperHandler(_requestsForHelpRepo, _activeHelpRepo, new Clock());
    }

    [Test]
    public async Task Should_accept_potential_helper()
    {
        var requestForHelpId = FakeRequestForHelpRepository.ExistedRequestForHelpId;
        var requestForHelp = await _requestsForHelpRepo.Get(requestForHelpId, _cancellationToken);
        var potentialHelper = AnyPotentialHelper();
        requestForHelp.ExpressInterest(potentialHelper);
        var command = new AcceptPotentialHelper(requestForHelpId, FakeRequestForHelpRepository.Needy, potentialHelper);

        await _handler.Handle(command, _cancellationToken);

        var activeHelp = await _activeHelpRepo.Get(new ActiveHelpId(requestForHelpId.Value), _cancellationToken);
        Assert.Multiple(() =>
        {
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
            Assert.That(activeHelp, Is.Not.Null);
        });
    }

    [Test]
    public void Should_not_accept_potential_helper_when_request_for_help_does_not_exist()
    {
        var command = new AcceptPotentialHelper(
            FakeRequestForHelpRepository.NotExistedRequestForHelpId,
            FakeRequestForHelpRepository.Needy,
            AnyPotentialHelper());

        Assert.ThrowsAsync<RequestForHelpDoesNotExist>(() => _handler.Handle(command, _cancellationToken));
    }

    private static PotentialHelper AnyPotentialHelper() => new(Guid.NewGuid());
}