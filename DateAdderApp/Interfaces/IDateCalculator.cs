using DateAdderApp.Models;

namespace DateAdderApp.Interfaces;

public interface IDateCalculator
{
    bool IsLeapYear(int year);
    int DaysInMonth(int month, int year);
    DateParts AddDays(DateParts date, int daysToAdd);
}