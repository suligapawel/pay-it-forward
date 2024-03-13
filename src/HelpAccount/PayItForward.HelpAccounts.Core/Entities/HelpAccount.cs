namespace PayItForward.HelpAccounts.Core.Entities;

public class HelpAccount
{
    public Guid AccountOwner { get; init; }
    public int Value { get; private set; }

    public HelpAccount(Guid accountOwner, int value)
    {
        AccountOwner = accountOwner;
        Value = value;
    }

    public void Incur(int value)
    {
        if (Value < 0)
        {
            return;
        }

        Value -= value;
    }

    public void PayOff(int value)
    {
        Value += value;
    }

    public bool CanIncurTheDebt() => Value >= 0;
}