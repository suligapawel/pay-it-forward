using PayItForward.AskingForHelps.Domain.Dicionaries;
using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.AskingForHelps.Domain.Aggregates;

public class ActiveHelp
{
    private readonly Helper _helper;
    private readonly DateTime _expiryDate;
    private ActiveHelpState _state;
    public ActiveHelpId Id { get; init; }

    public ActiveHelp(ActiveHelpId id, Helper helper, DateTime expiryDate, ActiveHelpState state)
    {
        Id = id;
        _helper = helper;
        _expiryDate = expiryDate;
        _state = state;
    }

    public HelpCompleted Complete(Helper helper, IClock clock)
    {
        if (!_helper.AmITheHelper(helper))
        {
            throw new TheHelperIsSomeoneElse(helper);
        }

        if (!IsActive())
        {
            throw new TheHelpIsNotActive(Id);
        }

        if (TimeIsUp(clock))
        {
            throw new TimeIsUp(Id.Value);
        }

        _state = ActiveHelpState.Completed;
        return new HelpCompleted(Id, helper);
    }

    public HelpAbandoned Abandon(Helper helper)
    {
        if (!_helper.AmITheHelper(helper))
        {
            throw new TheHelperIsSomeoneElse(helper);
        }
        
        if (!IsActive())
        {
            throw new TheHelpIsNotActive(Id);
        }
        
        _state = ActiveHelpState.Abandoned;

        return new HelpAbandoned(Id, helper);
    }

    public bool IsActive()
        => !_state.HasFlag(ActiveHelpState.Completed)
           && !_state.HasFlag(ActiveHelpState.Approved)
           && !_state.HasFlag(ActiveHelpState.Abandoned);

    private bool TimeIsUp(IClock clock) => _expiryDate < clock.Now;
}