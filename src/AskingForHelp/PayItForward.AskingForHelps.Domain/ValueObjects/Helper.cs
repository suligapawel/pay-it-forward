namespace PayItForward.AskingForHelps.Domain.ValueObjects;

public record Helper(Guid Id)
{
    public bool AmITheHelper(Helper helper)
        => Id == helper.Id;
}