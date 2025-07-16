using ShiftLogger.API.Models;

namespace ShiftLogger.API.Interfaces;
public interface IWorkerService
{
    public Worker CreateWorker(Worker worker);
    public List<Worker> GetAllWorkers();
    public Worker GetWorkerById(int id);
    public Worker UpdateWorker(int id, Worker updatedWorker);
    public string DeleteWorker(int id);
}
