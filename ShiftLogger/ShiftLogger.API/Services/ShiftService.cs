using ShiftLogger.API.Data;
using ShiftLogger.API.Models;

namespace ShiftLogger.API.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftLoggerDbContext _context;

    public ShiftService(ShiftLoggerDbContext context)
    {
        _context = context;
    }

    public Shift CreateShift(Shift shift)
    {
        var savedShift = _context.Add(shift);
        _context.SaveChanges();
        return savedShift.Entity;
    }

    public List<Shift> GetAllShiftsForWorker(int workerId)
    {
        return _context.Shifts.Where(sh => sh.WorkerId == workerId).ToList();
    }

    public Shift? GetUnfinishedShiftForWorker(int workerId)
    {
        return _context.Shifts.Where(sh => sh.WorkerId == workerId && string.IsNullOrEmpty(sh.EndTime)).First();
    }

    public Shift? UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = _context.Shifts.Find(id);

        if (savedShift == null)
        {
            return null;
        }

        _context.Entry(savedShift).CurrentValues.SetValues(updatedShift);
        _context.SaveChanges();

        return savedShift;
    }

    public string? DeleteShift(int id)
    {
        Shift? savedShift = _context.Shifts.Find(id);

        if (savedShift == null)
        {
            return null;            
        }

        _context.Shifts.Remove(savedShift);
        _context.SaveChanges();

        return $"Successfully deleted shift with id: {id}";

    }
}
