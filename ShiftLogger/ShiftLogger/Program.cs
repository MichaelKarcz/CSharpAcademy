using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Console.Services;
using ShiftLogger.Console.Controllers;
using Spectre.Console;

namespace ShiftLogger.Console;
public class Program()
{
    public static void Main(string[] args)
    {
        AnsiConsole.WriteLine("Starting App...\n\n");

        MainMenuController.RunMainMenuLoop();
        
        //TestPostWorker();

        //TestGetAllWorkers();
        //List<Worker> workers = ShiftLoggerAPIService.GetAllWorkers();
        //DisplayAllWorkers(workers);

        AnsiConsole.WriteLine("\n\nEnd of app...");
    }



    public static void TestGetAllWorkers()
    {
        List<WorkerResponse> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        foreach (WorkerResponse worker in allWorkers)
        {
            AnsiConsole.WriteLine(worker.Name);
        }
    }

    public static void TestPostWorker()
    {
        CreateWorkerRequest worker = new CreateWorkerRequest() { Name = "Test" };
        bool result = ShiftLoggerAPIService.CreateWorker(worker);

        AnsiConsole.WriteLine(result);
    }

    internal static void DisplayAllWorkers(List<WorkerResponse> workers)
    {
        foreach (WorkerResponse worker in workers)
        {
            AnsiConsole.WriteLine(worker.Name);
            if (worker.Shifts != null)
            {
                foreach (ShiftResponse shift in worker.Shifts)
                {
                    AnsiConsole.WriteLine("\tNew Shift");
                    AnsiConsole.WriteLine($"\tStart Time: {shift.StartTime.ToString("MM-dd-yyyy hh:mm tt")}");
                    if (shift.EndTime != null) AnsiConsole.WriteLine($"\tEnd Time: {shift.EndTime.Value.ToString("MM-dd-yyyy hh:mm tt")}");
                }
            }
        }
    }
}