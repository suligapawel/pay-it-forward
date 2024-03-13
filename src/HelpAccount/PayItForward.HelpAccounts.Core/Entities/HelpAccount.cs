namespace PayItForward.HelpAccounts.Core.Entities;

public sealed class HelpAccount
{
    public Guid AccountOwner { get; init; }
    public int Value { get; private set; }

    public HelpAccount(Guid accountOwner, int value)
    {
        AccountOwner = accountOwner;
        Value = value;
    }

    public void Incur()
    {
        if (Value < 0)
        {
            return;
        }

        Value -= 3;
    }

    public void PayOff()
    {
        Value++;
    }

    public bool CanIncurTheDebt() => Value >= 0;
}