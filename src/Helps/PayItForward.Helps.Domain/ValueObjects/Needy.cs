namespace PayItForward.Helps.Domain.ValueObjects;

public record Needy(Guid Id)
{
    public bool IsTheSamePersonAs(PotentialHelper potentialHelper)
        => Id == potentialHelper.Id;
}