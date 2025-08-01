using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Contracts.Worker;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;

namespace ShiftLogger.API.Services;

public class WorkerService : IWorkerService
{
    private readonly ShiftLoggerDbContext _context;

    public WorkerService(ShiftLoggerDbContext context)
    {
        _context = context;
    }

    public WorkerDto CreateWorker(Worker worker)
    {
        var addedWorker = _context.Workers.Add(worker);
        _context.SaveChanges();
        return addedWorker.Entity.ToDto();
    }

    public List<WorkerDto> GetAllWorkers()
    {
        List<Worker> resultsList = _context.Workers
            .Include(worker => worker.Shifts)
            .ToList();

        if (resultsList.Count == 0) return new List<WorkerDto>();

        return resultsList.Select(worker => worker.ToDto()).ToList();
    }

    public WorkerDto GetWorkerById(int id)
    {
        var results = _context.Workers
            .Include(worker => worker.Shifts)
            .Where(w => w.Id == id);

        if (results == null || results.Count() < 1)
        {
            return new WorkerDto();
        }
        return results.First().ToDto();
    }

    public WorkerDto GetWorkerByUsername(string username)
    {
        var results = _context.Workers
            .Include(worker => worker.Shifts)
            .Where(w => w.Username == username);

        if (results == null || results.Count() < 1)
        {
            return new WorkerDto();
        }

        return results.First().ToDto();
    }

    public WorkerDto UpdateWorker(int id, Worker updatedWorker)
    {
        Worker? savedWorker = _context.Workers.Find(id);

        if (savedWorker == null)
        {
            return new WorkerDto();
        }

        _context.Entry(savedWorker).CurrentValues.SetValues(updatedWorker);
        _context.SaveChanges();

        return savedWorker.ToDto();
    }

    public string DeleteWorker(int id)
    {
        Worker? savedWorker = _context.Workers.Find(id);

        if (savedWorker == null)
        {
            return string.Empty;
        }

        _context.Remove(savedWorker);
        _context.SaveChanges();

        return $"Successfully deleted worker with id: {id}";
    }
}
