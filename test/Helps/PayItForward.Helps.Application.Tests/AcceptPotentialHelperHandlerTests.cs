using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.Tests.Fixtures.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests;

public class AcceptPotentialHelperHandlerTests
{
    private AcceptPotentialHelperHandler _handler;
    private IRequestForHelpRepository _repo;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _cancellationToken = CancellationToken.None;
        _repo = new FakeRequestForHelpRepository();
        _handler = new AcceptPotentialHelperHandler(_repo);
    }

    [Test]
    public async Task Should_accept_potential_helper()
    {
        var requestForHelpId = FakeRequestForHelpRepository.ExistedRequestForHelpId;
        var requestForHelp = await _repo.Get(requestForHelpId, _cancellationToken);
        var potentialHelper = AnyPotentialHelper();
        requestForHelp.ExpressInterest(potentialHelper);
        var command = new AcceptPotentialHelper(requestForHelpId, FakeRequestForHelpRepository.Needy, potentialHelper);

        await _handler.Handle(command, _cancellationToken);

        // TODO: Check the active help
        // Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
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