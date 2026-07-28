using DateAdderApp.Interfaces;
using DateAdderApp.Interfaces;

namespace DateAdderApp.Services;

public class DateParser : IDateParser
{
    private readonly IDateCalculator _calculator;

    public DateParser(IDateCalculator calculator)
    {
        _calculator = calculator;
    }

    public bool TryParse(string input, out int day, out int month, out int year)
    {
        day = month = year = 0;

        if (input is not { Length: 10 }) return false;
        if (input[2] != '/' || input[5] != '/') return false;

        if (!TryParseSegment(input.AsSpan(0, 2), out day)) return false;
        if (!TryParseSegment(input.AsSpan(3, 2), out month)) return false;
        if (!TryParseSegment(input.AsSpan(6, 4), out year)) return false;

        if (year < 1 || month < 1 || month > 12) return false;
        if (day < 1 || day > _calculator.DaysInMonth(month, year)) return false;

        return true;
    }

    public string Format(int day, int month, int year) =>
        $"{day:D2}/{month:D2}/{year:D4}";

    private static bool TryParseSegment(ReadOnlySpan<char> span, out int result)
    {
        result = 0;
        foreach (char c in span)
        {
            if (c < '0' || c > '9') return false;
            result = result * 10 + (c - '0');
        }
        return true;
    }
}
