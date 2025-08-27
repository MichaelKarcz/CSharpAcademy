using Spectre.Console;
using System.Globalization;

namespace ShiftLogger.Console.Helpers;

internal static class InputHelper
{
    internal static DateTime GetValidDateTimeInput(string prompt)
    {
        DateTime validDateTime = new DateTime();

        string dateString;
        
        while (true)
        {
            dateString = AnsiConsole.Prompt(new TextPrompt<string>(prompt/*"Please enter the date and time in the format MM-dd-yyyy hh:mm tt, i.e. \"05-24-2025 05:22 am\""*/));

            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out validDateTime))
            {
                return validDateTime;
            }
            else AnsiConsole.WriteLine("Invalid format! Date/time input must be in format MM-dd-yyyy hh:mm tt.");    
        }
    }

    internal static ValidationResult ValidateDateTime(string dateTime)
    {
        if (!DateTime.TryParseExact(dateTime, "MM-dd-yyyy hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return ValidationResult.Error("Incorrect time format! Remember to format your entry as MM-dd-yyyy hh:mm tt, so '05-24-2025 05:22 PM' for example.\n");
        }

        DateTime timeDT = DateTime.ParseExact(dateTime, "MM-dd-yyyy hh:mm tt", new CultureInfo("en-US"));
        if (timeDT > DateTime.Now)
        {
            return ValidationResult.Error("You cannot log a future date/time.");
        }

        return ValidationResult.Success();
    }

    internal static ValidationResult ValidateEndTime(string endTime, string startTime)
    {
        ValidationResult validateGenericTimeResult = ValidateDateTime(endTime);

        if (!validateGenericTimeResult.Successful)
        {
            return validateGenericTimeResult;
        }

        DateTime endTimeDT = DateTime.ParseExact(endTime, "MM-dd-yyyy hh:mm tt", new CultureInfo("en-US"));
        DateTime startTimeDT = DateTime.ParseExact(startTime, "MM-dd-yyyy hh:mm tt", new CultureInfo("en-US"));

        if (endTimeDT < startTimeDT)
        {
            return ValidationResult.Error("You cannot have an end time earlier than the start time.\n");
        }

        return ValidationResult.Success();
    }
}
