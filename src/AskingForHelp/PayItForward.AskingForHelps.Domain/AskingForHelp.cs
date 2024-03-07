using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain;

public class AskingForHelp
{
    private readonly Needy _needy;
    private readonly List<PotentialHelper> _groupOfPotentialHelpers;
    public AskingForHelpId Id { get; init; }

    public AskingForHelp(AskingForHelpId id, Needy needy, IEnumerable<PotentialHelper> potentialHelpers)
    {
        Id = id;
        _needy = needy;
        _groupOfPotentialHelpers = potentialHelpers?.ToList() ?? [];
    }

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

        _groupOfPotentialHelpers.Add(potentialHelper);

        return new InterestExpressed(Id, potentialHelper);
    }

    public bool IsInGroupOfPotentialHelpers(PotentialHelper potentialHelper)
        => _groupOfPotentialHelpers.Contains(potentialHelper);
}