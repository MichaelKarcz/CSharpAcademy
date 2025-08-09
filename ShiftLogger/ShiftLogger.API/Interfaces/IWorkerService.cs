using ShiftLogger.API.Contracts.Worker;

namespace ShiftLogger.API.Interfaces;
public interface IWorkerService
{
    public WorkerDto CreateWorker(Worker worker);
    public List<WorkerDto> GetAllWorkers();
    public WorkerDto GetWorkerById(int id);
    public WorkerDto GetWorkerByUsername(string username);
    public WorkerDto UpdateWorker(int id, Worker updatedWorker);
    public string DeleteWorker(int id);
}
