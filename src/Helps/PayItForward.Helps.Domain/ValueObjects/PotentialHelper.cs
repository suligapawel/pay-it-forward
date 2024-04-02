namespace PayItForward.Helps.Domain.ValueObjects;

public record PotentialHelper(Guid Id)
{
    public bool IsTheSamePersonAs(PotentialHelper potentialHelper)
        => Id == potentialHelper.Id;
}