using DateAdderApp.Models;
using DateAdderApp.Services;
using Xunit;

namespace DateAdder.Tests.Services;

public class DateCalculatorTests
{
    private readonly DateCalculator _sut = new();

    [Theory]
    [InlineData(2024, true)]   // divisible by 4, not 100
    [InlineData(2000, true)]   // divisible by 400
    [InlineData(1900, false)]  // divisible by 100 but not 400
    [InlineData(2023, false)]  // not divisible by 4
    public void IsLeapYear_ReturnsCorrectResult(int year, bool expected)
    {
        Assert.Equal(expected, _sut.IsLeapYear(year));
    }

    [Theory]
    [InlineData(1, 2024, 31)]
    [InlineData(2, 2024, 29)]  // leap year Feb
    [InlineData(2, 2023, 28)]  // non-leap Feb
    [InlineData(4, 2023, 30)]
    [InlineData(12, 2023, 31)]
    public void DaysInMonth_ReturnsCorrectDays(int month, int year, int expected)
    {
        Assert.Equal(expected, _sut.DaysInMonth(month, year));
    }

    [Theory]
    [InlineData(31, 1, 2026, 1, 1, 2, 2026)]   // month rollover
    [InlineData(28, 2, 2024, 1, 29, 2, 2024)]   // leap year stays in Feb
    [InlineData(28, 2, 2023, 1, 1, 3, 2023)]   // non-leap Feb → March
    [InlineData(31, 12, 2025, 1, 1, 1, 2026)]   // year rollover
    [InlineData(15, 6, 2023, 0, 15, 6, 2023)]   // add zero days
    [InlineData(1, 1, 2023, 365, 1, 1, 2024)]   // full year (non-leap)
    [InlineData(1, 1, 2024, 366, 1, 1, 2025)]   // full leap year
    [InlineData(12, 12, 2026, -1, 11, 12, 2026)] // subtract one day
    [InlineData(1, 3, 2023, -1, 28, 2, 2023)]    // subtract into non-leap Feb
    [InlineData(1, 3, 2024, -1, 29, 2, 2024)]    // subtract into leap Feb
    [InlineData(1, 1, 2026, -1, 31, 12, 2025)]   // subtract across year boundary
    public void AddDays_ProducesCorrectDate(
        int startDay, int startMonth, int startYear, int days,
        int expectedDay, int expectedMonth, int expectedYear)
    {
        var result = _sut.AddDays(new DateParts(startDay, startMonth, startYear), days);
        Assert.Equal(expectedDay, result.Day);
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedYear, result.Year);
    }
}