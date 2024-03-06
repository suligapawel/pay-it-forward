using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Tests;

public class AskingForHelpTests
{
    [Test]
    public void Should_add_to_the_group_of_potential_helpers()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyNewAskingForHelp();

        var domainEvent = askingForHelp.ExpressInterest(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<InterestExpressed>());
            Assert.That(domainEvent.AskingForHelpId, Is.EqualTo(askingForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(askingForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.True);
        });
    }

    private static AskingForHelp AnyNewAskingForHelp()
        => new(new AskingForHelpId(Guid.NewGuid()), Enumerable.Empty<PotentialHelper>());

    private static PotentialHelper AnyPotentialHelper()
        => new(Guid.NewGuid());
}