using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Helps.Domain.Tests.Fixtures;

public class TestClock : IClock
{
    private DateTime _dateTimeForTests = new(2024, 03, 11, 22, 45, 30);

    public DateTime Now => _dateTimeForTests;
    public DateOnly Today => DateOnly.FromDateTime(_dateTimeForTests);

    public void Set(DateTime newDate) => _dateTimeForTests = newDate;
}