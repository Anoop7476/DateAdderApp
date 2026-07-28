using DateAdderApp.Models;
using DateAdderApp.Services;
using DateAdderApp.Validation;
using Xunit;

namespace DateAdder.Tests.Validation;

public class DateAddRequestValidatorTests
{
    private readonly DateAddRequestValidator _sut = new(new DateParser(new DateCalculator()));

    [Fact]
    public void Validate_ValidRequest_ReturnsValid()
    {
        var result = _sut.Validate(new DateAddRequest("31/01/2026", 1));
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_NullOrEmptyDate_ReturnsInvalid(string? date)
    {
        var result = _sut.Validate(new DateAddRequest(date!, 1));
        Assert.False(result.IsValid);
        Assert.Contains("required", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("not-a-date")]
    [InlineData("31-01-2026")]
    [InlineData("29/02/2023")]
    public void Validate_InvalidDateFormat_ReturnsInvalid(string date)
    {
        var result = _sut.Validate(new DateAddRequest(date, 1));
        Assert.False(result.IsValid);
        Assert.NotNull(result.ErrorMessage);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_NegativeDays_ReturnsValid(int days)
    {
        var result = _sut.Validate(new DateAddRequest("01/01/2026", days));
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_ZeroDays_ReturnsValid()
    {
        var result = _sut.Validate(new DateAddRequest("01/01/2026", 0));
        Assert.True(result.IsValid);
    }
}