using DateAdderApp.Interfaces;
using DateAdderApp.Models;

namespace DateAdderApp.Services;

public class DateCalculator : IDateCalculator
{
    private static readonly int[] MonthLengths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public bool IsLeapYear(int year) =>
        (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);

    public int DaysInMonth(int month, int year) =>
        month == 2 && IsLeapYear(year) ? 29 : MonthLengths[month - 1];

    public DateParts AddDays(DateParts date, int daysToAdd)
    {
        int day = date.Day, month = date.Month, year = date.Year;

        while (daysToAdd > 0)
        {
            int remaining = DaysInMonth(month, year) - day;
            if (daysToAdd <= remaining)
            {
                day += daysToAdd;
                break;
            }
            daysToAdd -= remaining + 1;
            day = 1;
            if (++month > 12) { month = 1; year++; }
        }

        while (daysToAdd < 0)
        {
            if (day > -daysToAdd)
            {
                day += daysToAdd;
                break;
            }
            daysToAdd += day;
            if (--month < 1) { month = 12; year--; }
            day = DaysInMonth(month, year);
        }

        return new DateParts(day, month, year);
    }
}
