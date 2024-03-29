using PayItForward.Helps.Domain.Events;
using PayItForward.Helps.Domain.Exceptions;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Aggregates;

public sealed class RequestForHelp
{
    private readonly Needy _needy;
    private readonly List<PotentialHelper> _groupOfPotentialHelpers;
    private readonly List<PotentialHelper> _abandoners;
    public RequestForHelpId Id { get; init; }

    public RequestForHelp(
        RequestForHelpId id,
        Needy needy,
        IEnumerable<PotentialHelper> potentialHelpers,
        IEnumerable<PotentialHelper> abandoners)
    {
        Id = id;
        _needy = needy;
        _groupOfPotentialHelpers = potentialHelpers?.ToList() ?? [];
        _abandoners = abandoners?.ToList() ?? [];
    }

    public static RequestForHelp New(RequestForHelpId id, Needy needy)
        => new(id, needy, null, null);

    public InterestExpressed ExpressInterest(PotentialHelper potentialHelper)
    {
        if (_needy.IsTheSamePersonAs(potentialHelper))
        {
            throw new PotentialHelperAndNeedyAreTheSamePerson(potentialHelper);
        }

        if (IsInTheGroupOfPotentialHelpers(potentialHelper))
        {
            throw new HasAlreadyExpressedInterest(potentialHelper);
        }

        if (_abandoners.Contains(potentialHelper))
        {
            throw new PotentialHelperAbandonedTheRequest(potentialHelper);
        }

        _groupOfPotentialHelpers.Add(potentialHelper);

        return new InterestExpressed(Id, potentialHelper);
    }

    public MindChanged DoNotHelp(PotentialHelper potentialHelper)
    {
        if (!IsInTheGroupOfPotentialHelpers(potentialHelper))
        {
            throw new TheLeaverDoesNotBelongToTheGroupOfPotentialHelpers(potentialHelper);
        }

        _groupOfPotentialHelpers.Remove(potentialHelper);

        return new MindChanged(Id, potentialHelper);
    }

    public void Accept(Needy needy, PotentialHelper potentialHelper)
    {
    }

    public bool IsInGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);

    private bool IsInTheGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);
}