using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Tests;

public class AskingForHelpTests
{
    [Test]
    public void Should_add_to_the_group_of_potential_helpers()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyAskingForHelp();

        var domainEvent = askingForHelp.ExpressInterest(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<InterestExpressed>());
            Assert.That(domainEvent.AskingForHelpId, Is.EqualTo(askingForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(askingForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.True);
        });
    }

    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_twice()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyAskingForHelp(new[] { potentialHelper });

        Assert.Throws<HasAlreadyExpressedInterest>(() => askingForHelp.ExpressInterest(potentialHelper));
    }

    private static AskingForHelp AnyAskingForHelp(IEnumerable<PotentialHelper> potentialHelpers = default)
        => new(new AskingForHelpId(Guid.NewGuid()), potentialHelpers);

    private static PotentialHelper AnyPotentialHelper()
        => new(Guid.NewGuid());
}