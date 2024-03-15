using PayItForward.Helps.Domain.Dicionaries;
using PayItForward.Helps.Domain.Events;
using PayItForward.Helps.Domain.Exceptions;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Helps.Domain.Aggregates;

public sealed class ActiveHelp
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
        return new HelpCompleted(Id, helper, clock.Now);
    }

    public HelpAbandoned Abandon(Helper helper, IClock clock)
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

        _state = ActiveHelpState.Abandoned;

        return new HelpAbandoned(Id, helper, clock.Now);
    }

    public bool IsActive()
        => !_state.HasFlag(ActiveHelpState.Completed) && !_state.HasFlag(ActiveHelpState.Abandoned);

    private bool TimeIsUp(IClock clock) => _expiryDate < clock.Now;
}