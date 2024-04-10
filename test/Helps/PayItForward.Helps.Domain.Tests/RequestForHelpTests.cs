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
    public void Should_not_add_to_the_group_of_potential_helpers_when_helper_is_already_chosen()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(potentialHelpers: [potentialHelper], chosenHelper: potentialHelper);

        Assert.Throws<SomeoneIsAlreadyHelpingWithThis>(() => requestForHelp.ExpressInterest(potentialHelper));
    }

    [Test]
    public void Should_do_not_help_but_leave_the_request_for_help_blocked_when_chosen_helper_is_someone_else()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(potentialHelpers: [potentialHelper], chosenHelper: AnyPotentialHelper());

        var domainEvent = requestForHelp.DoNotHelp(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<MindChanged>());
            Assert.That(domainEvent.RequestForHelpId, Is.EqualTo(requestForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
            Assert.That(requestForHelp.IsAccepted(), Is.True);
        });
    }

    [Test]
    public void Should_do_not_help_and_do_the_request_for_help_active_when_potential_helper_was_chosen()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(potentialHelpers: [potentialHelper], chosenHelper: potentialHelper);

        var domainEvent = requestForHelp.DoNotHelp(potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<MindChanged>());
            Assert.That(domainEvent.RequestForHelpId, Is.EqualTo(requestForHelp.Id));
            Assert.That(domainEvent.PotentialHelper, Is.EqualTo(potentialHelper));
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.False);
            Assert.That(requestForHelp.IsAccepted(), Is.False);
        });
    }

    [Test]
    public void Should_not_leave_the_group_of_potential_helpers_when_the_leaver_does_not_belong_there()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp();

        Assert.Throws<TheLeaverDoesNotBelongToTheGroupOfPotentialHelpers>(() => requestForHelp.DoNotHelp(potentialHelper));
    }

    [Test]
    public void Should_not_leave_the_group_of_potential_helpers_when_the_leaver_already_abandoned()
    {
        var potentialHelper = AnyPotentialHelper();
        var requestForHelp = AnyRequestForHelp(abandoners: [potentialHelper]);

        Assert.Throws<PotentialHelperAbandonedTheRequest>(() => requestForHelp.DoNotHelp(potentialHelper));
    }

    [Test]
    public void Should_accept_potential_helper()
    {
        var potentialHelper = AnyPotentialHelper();
        var needy = AnyNeedy();
        var requestForHelp = AnyRequestForHelp(needy: needy, potentialHelpers: new[] { potentialHelper });

        var domainEvent = requestForHelp.Accept(needy, potentialHelper);

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<HelpRequestAccepted>());
            Assert.That(requestForHelp.IsAccepted(), Is.True);
            Assert.That(requestForHelp.IsInGroupOfPotentialHelpers(potentialHelper), Is.True);
        });
    }

    [Test]
    public void Should_not_accept_potential_helper_when_potential_helper_is_not_in_the_group_of_potential_helpers()
    {
        var potentialHelper = AnyPotentialHelper();
        var needy = AnyNeedy();
        var requestForHelp = AnyRequestForHelp(needy: needy);

        Assert.Throws<PotentialHelperIsNotInTheGroupOfPotentialHelpers>(() => requestForHelp.Accept(needy, potentialHelper));
    }

    [Test]
    public void Should_not_accept_potential_helper_when_needy_is_not_an_owner_of_request_for_help()
    {
        var potentialHelper = AnyPotentialHelper();
        var needy = AnyNeedy();
        var requestForHelp = AnyRequestForHelp();

        Assert.Throws<NeedyIsNotOwner>(() => requestForHelp.Accept(needy, potentialHelper));
    }

    [Test]
    public void Should_not_accept_potential_helper_when_the_helper_is_chosen()
    {
        var potentialHelper = AnyPotentialHelper();
        var otherPotentialHelper = AnyPotentialHelper();
        var needy = AnyNeedy();
        var requestForHelp = AnyRequestForHelp(
            needy: needy, 
            potentialHelpers: [potentialHelper, otherPotentialHelper],
            chosenHelper: potentialHelper);

        Assert.Throws<SomeoneIsAlreadyHelpingWithThis>(() => requestForHelp.Accept(needy, otherPotentialHelper));
    }

    private static RequestForHelp AnyRequestForHelp(
        Needy needy = default,
        IEnumerable<PotentialHelper> potentialHelpers = default,
        IEnumerable<PotentialHelper> abandoners = default,
        PotentialHelper chosenHelper = default)
        => new(new RequestForHelpId(Guid.NewGuid()), needy ?? new Needy(Guid.NewGuid()), potentialHelpers, abandoners, chosenHelper);

    private static Needy AnyNeedy()
        => new(Guid.NewGuid());

    private static PotentialHelper AnyPotentialHelper()
        => new(Guid.NewGuid());
}