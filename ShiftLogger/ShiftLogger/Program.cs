using ShiftLogger.Models;
using ShiftLogger.Services;

namespace ShiftLogger;
public class Program()
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting App...\n\n");

        //TestPostWorker();

        //TestGetAllWorkers();
        List<Worker> workers = ShiftLoggerAPIService.GetAllWorkers();
        DisplayAllWorkers(workers);

        Console.WriteLine("\n\nEnd of app...");
    }



    public static void TestGetAllWorkers()
    {
        List<Worker> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        foreach (Worker worker in allWorkers)
        {
            Console.WriteLine(worker.Name);
        }
    }

    public static void TestPostWorker()
    {
        Worker worker = new Worker() { Name = "Test" };
        bool result = ShiftLoggerAPIService.CreateWorker(worker);

        Console.WriteLine(result);
    }

    internal static void DisplayAllWorkers(List<Worker> workers)
    {
        foreach (Worker worker in workers)
        {
            Console.WriteLine(worker.Name);
            if (worker.Shifts != null)
            {
                foreach (Shift shift in worker.Shifts)
                {
                    Console.WriteLine("\tNew Shift");
                    Console.WriteLine($"\tStart Time: {shift.StartTime.ToString("MM-dd-yyyy hh:mm tt")}");
                    if (shift.EndTime != null) Console.WriteLine($"\tEnd Time: {shift.EndTime.Value.ToString("MM-dd-yyyy hh:mm tt")}");
                }
            }
        }
    }
}