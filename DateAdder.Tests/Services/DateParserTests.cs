using DateAdderApp.Services;
using Xunit;

namespace DateAdder.Tests.Services;

public class DateParserTests
{
    private readonly DateParser _sut = new(new DateCalculator());

    [Theory]
    [InlineData("31/01/2026", 31, 1, 2026)]
    [InlineData("01/01/2000", 1, 1, 2000)]
    [InlineData("29/02/2024", 29, 2, 2024)]  // valid leap day
    [InlineData("28/02/2023", 28, 2, 2023)]
    public void TryParse_ValidDate_ReturnsTrueAndCorrectParts(
        string input, int expectedDay, int expectedMonth, int expectedYear)
    {
        var result = _sut.TryParse(input, out int d, out int m, out int y);
        Assert.True(result);
        Assert.Equal(expectedDay, d);
        Assert.Equal(expectedMonth, m);
        Assert.Equal(expectedYear, y);
    }

    [Theory]
    [InlineData("")]
    [InlineData("31-01-2026")]   // wrong separator
    [InlineData("31/13/2026")]   // invalid month
    [InlineData("29/02/2023")]   // Feb 29 in non-leap year
    [InlineData("00/01/2026")]   // day zero
    [InlineData("32/01/2026")]   // day out of range
    [InlineData("abc/mm/yyyy")]  // non-numeric
    [InlineData("1/1/2026")]     // wrong length
    public void TryParse_InvalidDate_ReturnsFalse(string input)
    {
        Assert.False(_sut.TryParse(input, out _, out _, out _));
    }

    [Theory]
    [InlineData(1, 2, 2026, "01/02/2026")]
    [InlineData(31, 12, 999, "31/12/0999")]
    public void Format_ReturnsCorrectString(int day, int month, int year, string expected)
    {
        Assert.Equal(expected, _sut.Format(day, month, year));
    }
}