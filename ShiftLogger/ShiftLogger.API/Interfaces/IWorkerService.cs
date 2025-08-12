using ShiftLogger.API.Contracts.Workers;

namespace ShiftLogger.API.Interfaces;
public interface IWorkerService
{
    public WorkerDto CreateWorker(Worker worker);
    public List<WorkerDto> GetAllWorkers();
    public WorkerDto GetWorkerById(int id);
    public WorkerDto UpdateWorker(int id, Worker updatedWorker);
    public string DeleteWorker(int id);
}
