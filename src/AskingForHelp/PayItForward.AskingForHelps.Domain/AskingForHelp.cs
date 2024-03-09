using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain;

public class AskingForHelp
{
    private readonly Needy _needy;
    private readonly List<PotentialHelper> _groupOfPotentialHelpers;
    private readonly List<PotentialHelper> _abandoners;
    public AskingForHelpId Id { get; init; }

    public AskingForHelp(
        AskingForHelpId id,
        Needy needy,
        IEnumerable<PotentialHelper> potentialHelpers,
        IEnumerable<PotentialHelper> abandoners)
    {
        Id = id;
        _needy = needy;
        _groupOfPotentialHelpers = potentialHelpers?.ToList() ?? [];
        _abandoners = abandoners?.ToList() ?? [];
    }

    public static AskingForHelp New(AskingForHelpId id, Needy needy)
        => new(id, needy, null, null);

    public InterestExpressed ExpressInterest(PotentialHelper potentialHelper)
    {
        if (_needy.IsTheSamePersonAs(potentialHelper))
        {
            throw new PotentialHelperAndNeedyAreTheSamePerson(potentialHelper);
        }

        if (_groupOfPotentialHelpers.Contains(potentialHelper))
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
        _groupOfPotentialHelpers.Remove(potentialHelper);

        return new MindChanged(Id, potentialHelper);
    }

    public bool IsInGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);
}