using PayItForward.Helps.Domain.Events;
using PayItForward.Helps.Domain.Exceptions;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Domain.Aggregates;

public sealed class RequestForHelp
{
    private readonly Needy _needy;
    private readonly List<PotentialHelper> _groupOfPotentialHelpers;
    private readonly List<PotentialHelper> _abandoners;
    private PotentialHelper _chosenHelper;
    public RequestForHelpId Id { get; init; }

    public RequestForHelp(
        RequestForHelpId id,
        Needy needy,
        IEnumerable<PotentialHelper> potentialHelpers,
        IEnumerable<PotentialHelper> abandoners,
        PotentialHelper chosenHelper)
    {
        Id = id;
        _needy = needy;
        _groupOfPotentialHelpers = potentialHelpers?.ToList() ?? [];
        _abandoners = abandoners?.ToList() ?? [];
        _chosenHelper = chosenHelper;
    }

    public static RequestForHelp New(RequestForHelpId id, Needy needy)
        => new(id, needy, null, null, null);

    public InterestExpressed ExpressInterest(PotentialHelper potentialHelper)
    {
        if (_needy.IsTheSamePersonAs(potentialHelper))
        {
            throw new PotentialHelperAndNeedyAreTheSamePerson(potentialHelper);
        }

        if (IsAccepted())
        {
            throw new SomeoneIsAlreadyHelpingWithThis();
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
        _chosenHelper = null;

        return new MindChanged(Id, potentialHelper);
    }

    public HelpRequestAccepted Accept(Needy needy, PotentialHelper potentialHelper)
    {
        _chosenHelper = potentialHelper;

        if (!IsInTheGroupOfPotentialHelpers(potentialHelper))
        {
            throw new PotentialHelperIsNotInTheGroupOfPotentialHelpers(potentialHelper);
        }

        return new HelpRequestAccepted(needy, potentialHelper);
    }

    public bool IsAccepted() => _chosenHelper is not null;

    public bool IsInGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);

    private bool IsInTheGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);
}