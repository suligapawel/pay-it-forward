namespace PayItForward.Shared.Kernel.Helpers;

public interface IClock
{
    DateTime Now { get; }
    DateOnly Today { get; }
}