using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Events;
using PayItForward.Helps.Domain.Exceptions;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Tests;

public class RequestForHelpTests
{
    [Test]
    public void Should_add_to_the_group_of_potential_helpers()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp();

        var domainEvent = requestForHelp.ExpressInterest(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<InterestExpressed>());
            Assert.That(domainEvent.RequestForHelpId, Is.EqualTo(requestForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.True);
        });
    }

    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_twice()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(potentialHelpers: new[] { potentialHelper });

        Assert.Throws<HasAlreadyExpressedInterest>(() => requestForHelp.ExpressInterest(potentialHelper));
    }

    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_when_potential_helper_is_needy()
    {
        var needy = AnyNeedy();
        var requestForHelp = AnyRequestForHelp(needy: needy);

        Assert.Throws<PotentialHelperAndNeedyAreTheSamePerson>(() => requestForHelp.ExpressInterest(new PotentialHelper(needy.Id)));
    }


    [Test]
    public void Should_not_add_to_the_group_of_potential_helpers_when_potential_helper_abandoned_the_ask()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(abandoners: new[] { potentialHelper });

        Assert.Throws<PotentialHelperAbandonedTheRequest>(() => requestForHelp.ExpressInterest(potentialHelper));
    }
    
    [Test]
    public void Should_do_not_help()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(potentialHelpers: [potentialHelper]);

        var domainEvent = requestForHelp.DoNotHelp(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<MindChanged>());
            Assert.That(domainEvent.RequestForHelpId, Is.EqualTo(requestForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
        });
    } 
    
    [Test]
    public void Dont_let_leave_the_group_of_potential_helpers_when_the_leaver_does_not_belong_there()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp();

        Assert.Throws<TheLeaverDoesNotBelongToTheGroupOfPotentialHelpers>(() => requestForHelp.DoNotHelp(potentialHelper));
    }

    private static RequestForHelp AnyRequestForHelp(
        Needy needy = default,
        IEnumerable<PotentialHelper> potentialHelpers = default,
        IEnumerable<PotentialHelper> abandoners = default)
        => new(new RequestForHelpId(Guid.NewGuid()), needy ?? new Needy(Guid.NewGuid()), potentialHelpers, abandoners);

    private static Needy AnyNeedy()
        => new(Guid.NewGuid());

    private static PotentialHelper AnyPotentialHelper()
        => new(Guid.NewGuid());
}