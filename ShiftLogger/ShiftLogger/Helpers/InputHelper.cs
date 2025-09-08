using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;
using System.Globalization;

namespace ShiftLogger.Console.Helpers;

internal static class InputHelper
{
    internal static DateTime GetValidStartDate(string prompt)
    {
        DateTime validDateTime = new DateTime();

        string dateString;
        
        while (true)
        {
            dateString = AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                .Validate<string>(n => ValidateDateTime(n)));

            if (DateTime.TryParseExact(dateString, DisplayHelper.DateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out validDateTime))
            {
                return validDateTime;
            }
        }
    }

    internal static DateTime GetValidEndDate(string prompt, DateTime startDateTime)
    {
        DateTime validDateTime = new DateTime();

        string dateString;

        while (true)
        {
            dateString = AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                .Validate<string>(n => ValidateEndDateTime(n, startDateTime)));

            if (DateTime.TryParseExact(dateString, DisplayHelper.DateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out validDateTime))
            {
                return validDateTime;
            }
        }
    }

    internal static ValidationResult ValidateDateTime(string dateTime)
    {
        if (!DateTime.TryParseExact(dateTime, DisplayHelper.DateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return ValidationResult.Error($"Incorrect time format! Remember to format your entry as {DisplayHelper.DateFormat}, so '{DisplayHelper.DateFormatExample}' for example.\n");
        }

        DateTime timeDT = DateTime.ParseExact(dateTime, DisplayHelper.DateFormat, new CultureInfo("en-US"));
        if (timeDT > DateTime.Now)
        {
            return ValidationResult.Error("You cannot log a future date/time.");
        }

        return ValidationResult.Success();
    }

    internal static ValidationResult ValidateEndDateTime(string endTime, DateTime startTime)
    {
        ValidationResult validateGenericTimeResult = ValidateDateTime(endTime);

        if (!validateGenericTimeResult.Successful)
        {
            return validateGenericTimeResult;
        }

        DateTime endTimeDT = DateTime.ParseExact(endTime, DisplayHelper.DateFormat, new CultureInfo("en-US"));

        if (endTimeDT < startTime)
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
