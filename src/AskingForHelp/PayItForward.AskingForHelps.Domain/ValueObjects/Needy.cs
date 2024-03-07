namespace PayItForward.AskingForHelps.Domain.ValueObjects;

public record Needy(Guid Id)
{
    public bool IsTheSamePersonAs(PotentialHelper potentialHelper)
        => Id == potentialHelper.Id;
}