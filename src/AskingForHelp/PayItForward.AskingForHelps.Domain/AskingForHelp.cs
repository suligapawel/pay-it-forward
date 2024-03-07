using PayItForward.AskingForHelps.Domain.Events;
using PayItForward.AskingForHelps.Domain.Exceptions;
using PayItForward.AskingForHelps.Domain.ValueObjects;

namespace PayItForward.AskingForHelps.Domain;

public class AskingForHelp
{
    private readonly List<PotentialHelper> _groupOfPotentialHelpers;
    public AskingForHelpId Id { get; init; }

    public AskingForHelp(AskingForHelpId id, IEnumerable<PotentialHelper> potentialHelpers)
    {
        _groupOfPotentialHelpers = potentialHelpers?.ToList() ?? [];
        Id = id;
    }

    public InterestExpressed ExpressInterest(PotentialHelper potentialHelper)
    {
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