using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Data;
using ShiftLogger.API.Models;

namespace ShiftLogger.API.Services
{
    public interface IWorkerService
    {
        public Worker CreateWorker(Worker worker);
        public List<Worker> GetAllWorkers();
        public Worker? GetWorkerById(int id);
        public Worker? UpdateWorker(int id, Worker updatedWorker);
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
            var addedWorker = _context.Workers.Add(worker);
            _context.SaveChanges();
            return addedWorker.Entity;
        }

        public List<Worker> GetAllWorkers()
        {
            return _context.Workers.ToList();
        }

        public Worker? GetWorkerById(int id)
        {
            Worker? savedWorker = _context.Workers.Find(id);

            if (savedWorker == null)
            {
                return null;
            }

            return savedWorker;
        }

        public Worker? UpdateWorker(int id, Worker updatedWorker)
        {
            Worker? savedWorker = _context.Workers.Find(id);

            if (savedWorker == null)
            {
                return null;
            }

            _context.Entry(savedWorker).CurrentValues.SetValues(updatedWorker);
            _context.SaveChanges();

            return savedWorker;
        }

        public string? DeleteWorker(int id)
        {
            Worker? savedWorker = _context.Workers.Find(id);

            if (savedWorker == null)
            {
                return null;
            }

            _context.Remove(savedWorker);
            _context.SaveChanges();

            return $"Successfully deleted worker with id: {id}";
        }
    }
}
