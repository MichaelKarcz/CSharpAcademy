using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;
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

    public Shift GetUnfinishedShiftForWorker(int workerId)
    {
        var results = _context.Shifts.Where(sh => sh.WorkerId == workerId && sh.EndTime == null);
        return results.Count() > 0 ? results.First() : new Shift(){ WorkerId = workerId };
    }

    public Shift UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = _context.Shifts.Find(id);

        if (savedShift == null)
        {
            return new Shift(){ WorkerId = 0 };
        }

        _context.Entry(savedShift).CurrentValues.SetValues(updatedShift);
        _context.SaveChanges();

        return savedShift;
    }

    public string DeleteShift(int id)
    {
        Shift? savedShift = _context.Shifts.Find(id);

        if (savedShift == null)
        {
            return string.Empty;            
        }

        _context.Shifts.Remove(savedShift);
        _context.SaveChanges();

        return $"Successfully deleted shift with id: {id}";

    }
}
