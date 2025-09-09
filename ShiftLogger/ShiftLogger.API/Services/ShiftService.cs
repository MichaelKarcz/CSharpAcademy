using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;
using ShiftLogger.API.Models.Shifts;

namespace ShiftLogger.API.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftLoggerDbContext _context;

    public ShiftService(ShiftLoggerDbContext context)
    {
        _context = context;
    }

    public async Task<Shift> CreateShift(Shift shift)
    {
        var savedShift = await _context.AddAsync(shift);
        _context.SaveChanges();
        return savedShift.Entity;
    }

    public async Task<Shift?> GetShiftById(int id)
    {
        var results = _context.Shifts
            .Where(sh => sh.Id == id);

        if (results == null || results.Count() < 1)
        {
            return null;
        }
        return await results.FirstOrDefaultAsync();
    }

    public async Task<List<Shift>> GetAllShiftsForWorker(int workerId)
    {
        List<Shift> resultsList = await _context.Shifts.Include(sh => sh.Worker).Where(sh => sh.WorkerId == workerId).ToListAsync();
        return resultsList;
    }

    public async Task<Shift?> GetUnfinishedShiftForWorker(int workerId)
    {
        var results = _context.Shifts.Include(sh => sh.Worker).Where(sh => sh.WorkerId == workerId && sh.EndTime == null);
        return results.Count() > 0 ? await results.FirstOrDefaultAsync() : null;
    }

    public async Task<Shift?> UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = await _context.Shifts.FindAsync(id);

        _context.Entry(savedShift).CurrentValues.SetValues(updatedShift);
        await _context.SaveChangesAsync();

        return savedShift;
    }

    public async Task<string> DeleteShift(int id)
    {
        Shift? savedShift = await _context.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            return string.Empty;            
        }

        _context.Shifts.Remove(savedShift);
        await _context.SaveChangesAsync();

        return $"Successfully deleted shift with id: {id}";

    }
}
