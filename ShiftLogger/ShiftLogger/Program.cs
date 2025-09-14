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
    public async static Task Main(string[] args)
    {
        AnsiConsole.WriteLine("Starting App...\n\n");

        AnsiConsole.Clear();
        await MainMenuController.RunMainMenuLoop();

        //TestPostWorker();

        //TestGetAllWorkers();
        //List<Worker> workers = ShiftLoggerApiService.GetAllWorkers();
        //DisplayAllWorkers(workers);


            AnsiConsole.WriteLine("\n\nEnd of app...");
    }



    public async static Task TestGetAllWorkers()
    {
        List<WorkerResponse> allWorkers = await ShiftLoggerApiService.GetAllWorkersAsync();

        foreach (WorkerResponse worker in allWorkers)
        {
            AnsiConsole.WriteLine(worker.Name);
        }
    }

    public async static Task TestPostWorker()
    {
        CreateWorkerRequest worker = new CreateWorkerRequest() { Name = "Test" };
        bool result = await ShiftLoggerApiService.CreateWorkerAsync(worker);

        AnsiConsole.WriteLine(result);
    }

    public async static Task TestUpdateWorker()
    {
        WorkerResponse? preUpdate = await ShiftLoggerApiService.GetWorkerByIdAsync(2);
        AnsiConsole.WriteLine($"Pre-update: Id = {preUpdate.Id}, Name = {preUpdate.Name}");

        UpdateWorkerRequest newReq = new UpdateWorkerRequest() { Id = 2, Name = "Josh" };
        AnsiConsole.WriteLine($"Testing update worker Id = {newReq.Id}, Name = {newReq.Name}");
        WorkerResponse? postUpdate = await ShiftLoggerApiService.UpdateWorkerAsync(newReq.Id, newReq);
        if (postUpdate != null)
        {
            AnsiConsole.WriteLine($"Post-update: Id = {postUpdate.Id}, Name = {postUpdate.Name}");
            WorkerResponse? postUpdateGet = await ShiftLoggerApiService.GetWorkerByIdAsync(2);
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