using ShiftLogger.API.Data;
using ShiftLogger.API.Models;

namespace ShiftLogger.API.Services
{
    public interface IWorkerService
    {
        public List<Worker> GetAllWorkers();
        public Worker GetWorkerById(int id);
        public Worker CreateWorker(Worker worker);
        public Worker UpdateWorker(int id, Worker updatedWorker);
        public string? DeleteWorker(int id);
    }

    public class WorkerService : IWorkerService
    {
        private readonly ShiftLoggerDbContext _context;

        public WorkerService(ShiftLoggerDbContext context)
        {
            _context = context;
        }

        public Worker CreateWorker(Worker worker)
        {
            throw new NotImplementedException();
        }

        public string? DeleteWorker(int id)
        {
            throw new NotImplementedException();
        }

        public List<Worker> GetAllWorkers()
        {
            throw new NotImplementedException();
        }

        public Worker GetWorkerById(int id)
        {
            throw new NotImplementedException();
        }

        public Worker UpdateWorker(int id, Worker updatedWorker)
        {
            throw new NotImplementedException();
        }
    }
}
