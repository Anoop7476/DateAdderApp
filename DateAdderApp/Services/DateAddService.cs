using DateAdderApp.Interfaces;
using DateAdderApp.Models;
using DateAdderApp.Interfaces;
using DateAdderApp.Models;

namespace DateAdderApp.Services;

public class DateAddService : IDateAddService
{
    private readonly IDateParser _parser;
    private readonly IDateCalculator _calculator;

    public DateAddService(IDateParser parser, IDateCalculator calculator)
    {
        _parser = parser;
        _calculator = calculator;
    }

    public DateAddResponse AddDays(DateAddRequest request)
    {
        _parser.TryParse(request.Date, out int day, out int month, out int year);

        _calculator.AddDays(ref day, ref month, ref year, request.Days);

        return new DateAddResponse(
            OriginalDate: request.Date,
            DaysAdded: request.Days,
            NewDate: _parser.Format(day, month, year)
        );
    }
}
