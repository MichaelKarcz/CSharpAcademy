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

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        var savedShift = await _context.AddAsync(shift);
        _context.SaveChanges();
        return savedShift.Entity;
    }

    public async Task<Shift?> GetShiftByIdAsync(int id)
    {
        var results = _context.Shifts
            .Where(sh => sh.Id == id);

        return await results.FirstOrDefaultAsync();
    }

    public async Task<List<Shift>> GetAllShiftsForWorkerAsync(int workerId)
    {
        List<Shift> resultsList = await _context.Shifts.Include(sh => sh.Worker).Where(sh => sh.WorkerId == workerId).ToListAsync();
        return resultsList;
    }

    public async Task<Shift?> GetUnfinishedShiftForWorkerAsync(int workerId)
    {
        var results = _context.Shifts.Include(sh => sh.Worker).Where(sh => sh.WorkerId == workerId && sh.EndTime == null);
        return results.Count() > 0 ? await results.FirstOrDefaultAsync() : null;
    }

    public async Task<Shift?> UpdateShiftAsync(int id, Shift updatedShift)
    {
        Shift? savedShift = await _context.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            return null;
        }

        _context.Entry(savedShift).CurrentValues.SetValues(updatedShift);
        await _context.SaveChangesAsync();

        return savedShift;
    }

    public async Task<string> DeleteShiftAsync(int id)
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
