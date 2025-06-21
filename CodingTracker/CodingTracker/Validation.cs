using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    internal static class Validation
    {
        internal static ValidationResult ValidateDate(string date)
        {
            bool dateValid = false;

            if (!DateTime.TryParseExact(date, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                return ValidationResult.Error("Incorrect date format! Remember to format your entry as MM-dd-yyyy, so \"05-24-2025\" for example.\n");
            }

            DateTime dateDT = DateTime.ParseExact(date, "MM-dd-yyyy", new CultureInfo("en-US"));
            if (dateDT > DateTime.Now)
            {
                return ValidationResult.Error("You cannot log a future session.\n");
            }

            return ValidationResult.Success();
        }

        internal static ValidationResult ValidateGenericTime(string time)
        {
            bool timeValid = false;

            if (!DateTime.TryParseExact(time, "hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                return ValidationResult.Error("Incorrect time format! Remember to format your entry as hh:mm tt, so \"05:22 PM\" for example.\n");
            }

            return ValidationResult.Success();
        }

        internal static ValidationResult ValidateEndTime(string endTime, string startTime)
        {
            ValidationResult validateGenericTimeResult = ValidateGenericTime(endTime);

            if (!validateGenericTimeResult.Successful)
            {
                return validateGenericTimeResult;
            }

            DateTime endTimeDT = DateTime.ParseExact(endTime, "hh:mm tt", new CultureInfo("en-US"));
            DateTime startTimeDT = DateTime.ParseExact(startTime, "hh:mm tt", new CultureInfo("en-US"));

            if (endTimeDT < startTimeDT)
            {
                return ValidationResult.Error("You cannot have an end time earlier than the start time.\n");
            }

            return ValidationResult.Success();
        }

    }
}
