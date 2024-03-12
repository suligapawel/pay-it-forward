namespace PayItForward.AskingForHelps.Domain.Dicionaries;

[Flags]
public enum ActiveHelpState
{
    Active = 0,
    Completed = 1 << 0,
    Approved = 1 << 1,
    Abandoned = 1 << 2,
}