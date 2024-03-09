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
        var askingForHelp = AnyAskingForHelp(potentialHelpers: new[] { potentialHelper });

        Assert.Throws<HasAlreadyExpressedInterest>(() => askingForHelp.ExpressInterest(potentialHelper));
    }

    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_when_potential_helper_is_needy()
    {
        var needy = AnyNeedy();
        var askingForHelp = AnyAskingForHelp(needy: needy);

        Assert.Throws<PotentialHelperAndNeedyAreTheSamePerson>(() => askingForHelp.ExpressInterest(new PotentialHelper(needy.Id)));
    }


    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_when_potential_helper_abandoned_the_ask()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyAskingForHelp(abandoners: new[] { potentialHelper });

        Assert.Throws<PotentialHelperAbandonedTheRequest>(() => askingForHelp.ExpressInterest(potentialHelper));
    }
    
    [Test]
    public void Should_do_not_help()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyAskingForHelp(potentialHelpers: [potentialHelper]);

        var domainEvent = askingForHelp.DoNotHelp(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<MindChanged>());
            Assert.That(domainEvent.AskingForHelpId, Is.EqualTo(askingForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(askingForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
        });
    } 
    
    [Test]
    public void Dont_let_leave_the_group_of_potential_helpers_when_the_leaver_does_not_belong_there()
    {
        var potentialHelper = AnyPotentialHelper();
        var askingForHelp = AnyAskingForHelp();

        Assert.Throws<TheLeaverDoesNotBelongToTheGroupOfPotentialHelpers>(() => askingForHelp.DoNotHelp(potentialHelper));
    }

    private static AskingForHelp AnyAskingForHelp(
        Needy needy = default,
        IEnumerable<PotentialHelper> potentialHelpers = default,
        IEnumerable<PotentialHelper> abandoners = default)
        => new(new AskingForHelpId(Guid.NewGuid()), needy ?? new Needy(Guid.NewGuid()), potentialHelpers, abandoners);

    private static Needy AnyNeedy()
        => new(Guid.NewGuid());

    private static PotentialHelper AnyPotentialHelper()
        => new(Guid.NewGuid());
}