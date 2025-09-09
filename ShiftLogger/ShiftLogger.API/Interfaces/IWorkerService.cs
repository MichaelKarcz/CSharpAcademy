using ShiftLogger.API.Models.Workers;

namespace ShiftLogger.API.Interfaces;
public interface IWorkerService
{
    public Task<Worker> CreateWorkerAsync(Worker worker);
    public Task<List<Worker>> GetAllWorkersAsync();
    public Task<Worker?> GetWorkerByIdAsync(int id);
    public Task<Worker?> UpdateWorkerAsync(int id, Worker updatedWorker);
    public Task<string> DeleteWorkerAsync(int id);
}
