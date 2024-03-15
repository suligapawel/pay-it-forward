namespace PayItForward.Helps.Domain.Dicionaries;

[Flags]
public enum ActiveHelpState
{
    Active = 0,
    Completed = 1 << 0,
    Abandoned = 1 << 1,
}