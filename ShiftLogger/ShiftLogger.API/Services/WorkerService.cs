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

    public async Task<Worker> CreateWorkerAsync(Worker worker)
    {
        var addedWorker = _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        return addedWorker.Entity;
    }

    public async Task<List<Worker>> GetAllWorkersAsync()
    {
        List<Worker> resultsList = await _context.Workers
            .Include(worker => worker.Shifts)
            .ToListAsync();

        if (resultsList.Count == 0) return new List<Worker>();

        return resultsList;
    }

    public async Task<Worker?> GetWorkerByIdAsync(int id)
    {
        var results = _context.Workers
            .Include(worker => worker.Shifts)
            .Where(w => w.Id == id);

        return await results.FirstOrDefaultAsync();
    }

    public async Task<Worker?> UpdateWorkerAsync(int id, Worker updatedWorker)
    {
        Worker? savedWorker = await _context.Workers.FindAsync(id);

        if (savedWorker == null)
        {
            return null;
        }

        _context.Entry(savedWorker).CurrentValues.SetValues(updatedWorker);
        await _context.SaveChangesAsync();

        return savedWorker;
    }

    public async Task<string> DeleteWorkerAsync(int id)
    {
        Worker? savedWorker = await _context.Workers.FindAsync(id);

        if (savedWorker == null)
        {
            return string.Empty;
        }

        _context.Remove(savedWorker);
        await _context.SaveChangesAsync();

        return $"Successfully deleted worker with id: {id}";
    }
}
