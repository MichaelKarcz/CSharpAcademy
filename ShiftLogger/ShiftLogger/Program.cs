using ShiftLogger.Controllers;
using ShiftLogger.Services;
using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Contracts.Requests.Shifts;

namespace ShiftLogger;
public class Program()
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting App...\n\n");

        MenuController.RunMainMenuLoop();
        
        //TestPostWorker();

        //TestGetAllWorkers();
        //List<Worker> workers = ShiftLoggerAPIService.GetAllWorkers();
        //DisplayAllWorkers(workers);

        Console.WriteLine("\n\nEnd of app...");
    }



    public static void TestGetAllWorkers()
    {
        List<WorkerResponse> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        foreach (WorkerResponse worker in allWorkers)
        {
            Console.WriteLine(worker.Name);
        }
    }

    public static void TestPostWorker()
    {
        CreateWorkerRequest worker = new CreateWorkerRequest() { Name = "Test" };
        bool result = ShiftLoggerAPIService.CreateWorker(worker);

        Console.WriteLine(result);
    }

    internal static void DisplayAllWorkers(List<WorkerResponse> workers)
    {
        foreach (WorkerResponse worker in workers)
        {
            Console.WriteLine(worker.Name);
            if (worker.Shifts != null)
            {
                foreach (ShiftResponse shift in worker.Shifts)
                {
                    Console.WriteLine("\tNew Shift");
                    Console.WriteLine($"\tStart Time: {shift.StartTime.ToString("MM-dd-yyyy hh:mm tt")}");
                    if (shift.EndTime != null) Console.WriteLine($"\tEnd Time: {shift.EndTime.Value.ToString("MM-dd-yyyy hh:mm tt")}");
                }
            }
        }
    }
}