using PayItForward.HelpAccounts.Core.Entities;

namespace PayItForward.Debts.Core.Tests;

public class HelpAccountTests
{
    [Test]
    public void Should_incur_the_debt()
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), 0);

        helpAccount.Incur(3);

        Assert.That(helpAccount.Value, Is.EqualTo(-3));
    }

    [Test]
    public void Should_not_incur_the_debt_when_account_owner_has_the_debt()
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), -1);

        helpAccount.Incur(3);

        Assert.That(helpAccount.Value, Is.EqualTo(-1));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Should_can_incur_the_debt(int debtValue)
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), debtValue);

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
    
    [TestCase(-1, 1, 0)]
    [TestCase(2, 1, 3)]
    public void Should_pay_off_the_debt(int actualDebtValue, int payOffValue, int result)
    {
        var helpAccount = new HelpAccount(Guid.NewGuid(), actualDebtValue);

        helpAccount.PayOff(payOffValue);

        Assert.That(helpAccount.Value, Is.EqualTo(result));
    }
}