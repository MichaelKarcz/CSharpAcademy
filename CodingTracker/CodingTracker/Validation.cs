using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    internal static class Validation
    {
        internal static ValidationResult validateDate(string date)
        {
            bool dateValid = false;

            if (!DateTime.TryParseExact(date, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                return ValidationResult.Error("Incorrect date format! Please enter a date in the format of MM-dd-yyyy");
            }

            return ValidationResult.Success();
        }

        internal static ValidationResult validateTime(string time)
        {
            bool timeValid = false;

            if (!DateTime.TryParseExact(time, "hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                return ValidationResult.Error("Incorrect time format! Please enter a time in the format of hh:mm tt where tt is AM or PM");
            }

            return ValidationResult.Success();
        }

    }
}
