using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;
using ShiftLogger.API.Models.Workers;

namespace ShiftLogger.API.Services;

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
        List<Worker> resultsList = _context.Workers
            .Include(worker => worker.Shifts)
            .ToList();

        if (resultsList.Count == 0) return new List<Worker>();

        return resultsList;
    }

    public Worker? GetWorkerById(int id)
    {
        var results = _context.Workers
            .Include(worker => worker.Shifts)
            .Where(w => w.Id == id);

        if (results == null || results.Count() < 1)
        {
            return null;
        }
        return results.First();
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
