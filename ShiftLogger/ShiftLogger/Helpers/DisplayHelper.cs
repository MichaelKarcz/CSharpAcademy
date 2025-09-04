using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Helpers;

internal static class DisplayHelper
{
    internal static void ViewAllWorkers()
    {
        List<WorkerResponse> allWorkers = ShiftLoggerApiService.GetAllWorkers();
        ViewWorkers(allWorkers);
    }

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
            table.AddRow(shift.Id.ToString(), shift.StartTime.ToString("MM-dd-yyyy hh:mm tt"), (shift.EndTime.HasValue ? shift.EndTime.Value.ToString("MM-dd-yyyy hh:mm tt") : ""));
        }
        table.Border(TableBorder.Heavy);
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }
}
