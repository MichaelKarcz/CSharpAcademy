using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;
using System.Globalization;

namespace ShiftLogger.Console.Helpers;

internal static class InputHelper
{
    private static string _dateFormat = "MM-dd-yyyy hh:mm tt";
    private static string _dateFormatExample = "05-24-2025 05:22 PM";

    internal static DateTime GetValidDateTimeInput(string prompt)
    {
        DateTime validDateTime = new DateTime();

        string dateString;
        
        while (true)
        {
            dateString = AnsiConsole.Prompt(new TextPrompt<string>(prompt));

            if (DateTime.TryParseExact(dateString, _dateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out validDateTime))
            {
                return validDateTime;
            }
            else AnsiConsole.WriteLine($"Invalid format! Date/time input must be in format {_dateFormat}.");
        }
    }

    internal static ValidationResult ValidateDateTime(string dateTime)
    {
        if (!DateTime.TryParseExact(dateTime, _dateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return ValidationResult.Error($"Incorrect time format! Remember to format your entry as {_dateFormat}, so '{_dateFormatExample}' for example.\n");
        }

        DateTime timeDT = DateTime.ParseExact(dateTime, _dateFormat, new CultureInfo("en-US"));
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

        DateTime endTimeDT = DateTime.ParseExact(endTime, _dateFormat, new CultureInfo("en-US"));
        DateTime startTimeDT = DateTime.ParseExact(startTime, _dateFormat, new CultureInfo("en-US"));

        if (endTimeDT < startTimeDT)
        {
            return ValidationResult.Error("You cannot have an end time earlier than the start time.\n");
        }

        return ValidationResult.Success();
    }

    internal static WorkerResponse? SelectAWorker(List<WorkerResponse> workers)
    {
        if (workers == null || workers.Count == 0)
        {
            AnsiConsole.WriteLine("There are no workers to choose from for this operation.");
            return null;
        }

        WorkerResponse selectedWorker = AnsiConsole.Prompt(
            new SelectionPrompt<WorkerResponse>()
            .Title("Select a worker:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more workers)[/]")
            .AddChoices(workers.ToArray())
        );

        return selectedWorker;
    }

    internal static ShiftResponse SelectAShift(List<ShiftResponse> shifts)
    {
        ShiftResponse selectedShift = AnsiConsole.Prompt(
            new SelectionPrompt<ShiftResponse>()
            .Title("Select a shift:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more shifts)[/]")
            .AddChoices(shifts.ToArray())
        );

        return selectedShift;
    }
}
