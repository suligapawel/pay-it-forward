using PayItForward.HelpAccounts.Core.Entities;

namespace PayItForward.Debts.Core.Tests;

public class HelpAccountTests
{
    [TestCase(0)]
    [TestCase(1)]
    public void Should_incur_the_debt(int actualAccountValue)
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), actualAccountValue);

        helpAccount.Incur();

        Assert.That(helpAccount.Value, Is.EqualTo(actualAccountValue - 3));
    }

    [Test]
    public void Should_not_incur_the_debt_when_account_owner_has_the_debt()
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), -1);

        helpAccount.Incur();

        Assert.That(helpAccount.Value, Is.EqualTo(-1));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Should_can_incur_the_debt(int actualAccountValue)
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), actualAccountValue);

        var result = helpAccount.CanIncurTheDebt();

        Assert.That(result, Is.True);
    }

    [Test]
    public void Should_cannot_incur_the_debt()
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), -1);

        var result = helpAccount.CanIncurTheDebt();

        Assert.That(result, Is.False);
    }

    [TestCase(-1)]
    [TestCase(2)]
    public void Should_pay_off_the_debt(int actualAccountValue)
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), actualAccountValue);

        helpAccount.PayOff();

        Assert.That(helpAccount.Value, Is.EqualTo(actualAccountValue + 1));
    }
}