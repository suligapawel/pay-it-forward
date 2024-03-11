using PayItForward.AskingForHelps.Domain.Aggregates;
using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.Tests.Fixtures;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain.Tests;

public class ActiveHelpTests
{
    [Test]
    public void Should_complete_the_active_help()
    {
        var helper = AnyHelper();
        var activeHelp = AnyActiveHelp(helper: helper);

        var domainEvent = activeHelp.CompleteBy(helper, new TestClock());

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent, Is.TypeOf<HelpCompleted>());
            Assert.That(domainEvent.ActiveHelpId, Is.EqualTo(activeHelp.Id));
            Assert.That(domainEvent.Helper, Is.EqualTo(helper));
            Assert.That(activeHelp.IsCompleted(), Is.True);
        });
    }

    [Test]
    public void Should_not_complete_the_active_help_when_time_is_up()
    {
        var helper = AnyHelper();
        var expiryDate = new DateTime(2024, 03, 10, 23, 56, 00);
        var activeHelp = AnyActiveHelp(helper: helper, expiryDate: expiryDate);

        Assert.Throws<TimeIsUp>(() => activeHelp.CompleteBy(helper, new TestClock()));
    }


    [Test]
    public void Should_not_complete_the_active_help_when_someone_else_tries_complete_the_help()
    {
        var activeHelp = AnyActiveHelp();
        var helper = AnyHelper();

        Assert.Throws<TheHelperIsSomeoneElse>(() => activeHelp.CompleteBy(helper, new TestClock()));
    }

    private static ActiveHelp AnyActiveHelp(Helper helper = null, DateTime expiryDate = default)
        => new(
            new ActiveHelpId(Guid.NewGuid()),
            helper ?? AnyHelper(),
            expiryDate == default ? AnyNotExpiredDate() : expiryDate);

    private static Helper AnyHelper() => new(Guid.NewGuid());
    private static DateTime AnyNotExpiredDate() => new(2024, 03, 12, 23, 56, 00);
}