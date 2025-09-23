using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;
using System.Net;

namespace ShiftLogger.Console.Helpers;

internal static class DisplayHelper
{
    public static string DateFormat = "MM-dd-yyyy hh:mm tt";
    public static string DateFormatExample = "05-24-2025 05:22 PM";

    internal static void ViewWorkers(List<WorkerResponse> workers)
    {
        AnsiConsole.Clear();
        if (workers == null || workers.Count ==  0)
        {
            AnsiConsole.WriteLine("\n\nThere are no workers to view.\n\n");
            return;
        }

        Table table = new Table();
        table.AddColumn(new TableColumn("Id").Centered().NoWrap());
        table.AddColumn(new TableColumn("Name").Centered().NoWrap());
        foreach (WorkerResponse worker in workers)
        {
            table.AddRow(worker.Id.ToString(), worker.Name);
        }
        table.Border(TableBorder.Heavy);
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    internal static void ViewShifts(List<ShiftResponse> shifts)
    {
        AnsiConsole.Clear();
        if (shifts == null || shifts.Count == 0)
        {
            AnsiConsole.WriteLine("\n\nThere are no shifts to view.\n\n");
            return;
        }

        Table table = new Table();
        table.AddColumn(new TableColumn("Id").Centered().NoWrap());
        table.AddColumn(new TableColumn("Start Time").Centered().NoWrap());
        table.AddColumn(new TableColumn("End Time").Centered().NoWrap());
        foreach (ShiftResponse shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.StartTime.ToString(DateFormat), (shift.EndTime.HasValue ? shift.EndTime.Value.ToString(DateFormat) : ""));
        }
        table.Border(TableBorder.Heavy);
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    internal static string GetFormattedErrorMessage(HttpStatusCode statusCode, string? responseContent)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => $"Invalid data: {responseContent}",
            HttpStatusCode.Conflict => "Entity already exists",
            HttpStatusCode.UnprocessableEntity => "Entity data failed validation",
            HttpStatusCode.Unauthorized => "Authentication required to create entity",
            HttpStatusCode.Forbidden => "Access forbidden - cannot create entity",
            HttpStatusCode.InternalServerError => "Server error occurred while creating entity",
            _ => $"Unexpected response trying to create entity: {statusCode}"
        };
    }
}
