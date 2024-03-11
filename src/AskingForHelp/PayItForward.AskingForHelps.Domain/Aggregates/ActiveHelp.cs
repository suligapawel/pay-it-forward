using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.AskingForHelps.Domain.Aggregates;

public class ActiveHelp
{
    private readonly DateTime _expiryDate;
    private bool _completed;
    public ActiveHelpId Id { get; init; }

    public ActiveHelp(ActiveHelpId id, DateTime expiryDate)
    {
        _expiryDate = expiryDate;
        Id = id;
    }

    public HelpCompleted CompleteBy(Helper helper, IClock clock)
    {
        _completed = true;

        if (TimeIsUp(clock))
        {
            throw new TimeIsUp(Id.Value);
        }

        return new HelpCompleted(Id, helper);
    }

    public bool IsCompleted() => _completed;

    private bool TimeIsUp(IClock clock) => _expiryDate < clock.Now;
}