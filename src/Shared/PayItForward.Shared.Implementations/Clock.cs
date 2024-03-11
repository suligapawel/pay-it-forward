using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Shared.Implementations;

public class Clock : IClock
{
    public DateTime Now => DateTime.Now;
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
}