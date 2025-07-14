using ShiftLogger.Models;
using ShiftLogger.Services;

namespace ShiftLogger;
public class Program()
{
    public static void Main(string[] args)
    {

        List<Worker> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        foreach (Worker worker in allWorkers)
        {
            Console.WriteLine(worker.Name);
        }

    }
}