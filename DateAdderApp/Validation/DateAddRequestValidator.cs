using DateAdderApp.Interfaces;
using DateAdderApp.Models;

namespace DateAdderApp.Validation;

public class DateAddRequestValidator : IDateAddRequestValidator
{
    private readonly IDateParser _parser;

    public DateAddRequestValidator(IDateParser parser)
    {
        _parser = parser;
    }

    public ValidationResult Validate(DateAddRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Date))
            return ValidationResult.Fail("Date is required.");

        if (!_parser.TryParse(request.Date, out _, out _, out _))
            return ValidationResult.Fail($"'{request.Date}' is not a valid date. Use dd/mm/yyyy.");

        return ValidationResult.Ok();
    }
}
