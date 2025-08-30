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

        AnsiConsole.Clear();
        MainMenuController.RunMainMenuLoop();

        //TestPostWorker();

        //TestGetAllWorkers();
        //List<Worker> workers = ShiftLoggerApiService.GetAllWorkers();
        //DisplayAllWorkers(workers);


            AnsiConsole.WriteLine("\n\nEnd of app...");
    }



    public static void TestGetAllWorkers()
    {
        List<WorkerResponse> allWorkers = ShiftLoggerApiService.GetAllWorkers();

        foreach (WorkerResponse worker in allWorkers)
        {
            AnsiConsole.WriteLine(worker.Name);
        }
    }

    public static void TestPostWorker()
    {
        CreateWorkerRequest worker = new CreateWorkerRequest() { Name = "Test" };
        bool result = ShiftLoggerApiService.CreateWorker(worker);

        AnsiConsole.WriteLine(result);
    }

    public static void TestUpdateWorker()
    {
        WorkerResponse preUpdate = ShiftLoggerApiService.GetWorkerById(2);
        AnsiConsole.WriteLine($"Pre-update: Id = {preUpdate.Id}, Name = {preUpdate.Name}");

        UpdateWorkerRequest newReq = new UpdateWorkerRequest() { Id = 2, Name = "Josh" };
        AnsiConsole.WriteLine($"Testing update worker Id = {newReq.Id}, Name = {newReq.Name}");
        WorkerResponse postUpdate = ShiftLoggerApiService.UpdateWorker(newReq.Id, newReq);
        if (postUpdate != null)
        {
            AnsiConsole.WriteLine($"Post-update: Id = {postUpdate.Id}, Name = {postUpdate.Name}");
            WorkerResponse postUpdateGet = ShiftLoggerApiService.GetWorkerById(2);
            AnsiConsole.WriteLine($"Post-update Get: Id = {postUpdateGet.Id}, Name = {postUpdateGet.Name}");
        }
        else AnsiConsole.WriteLine("Null returned from ShiftLoggerApiService.UpdateWorker");
    }

    internal static void DisplayAllWorkers(List<WorkerWithShiftsResponse> workers)
    {
        foreach (WorkerWithShiftsResponse worker in workers)
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