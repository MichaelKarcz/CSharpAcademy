using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Contracts.Shifts;
using ShiftLogger.API.Data;
using ShiftLogger.API.Interfaces;

namespace ShiftLogger.API.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftLoggerDbContext _context;

    public ShiftService(ShiftLoggerDbContext context)
    {
        _context = context;
    }

    public ShiftDto CreateShift(Shift shift)
    {
        var savedShift = _context.Add(shift);
        _context.SaveChanges();
        return savedShift.Entity.ToDto();
    }

    public List<ShiftDto> GetAllShiftsForWorker(int workerId)
    {
        List<Shift> resultsList = _context.Shifts.Where(sh => sh.WorkerId == workerId).ToList();
        return resultsList.Select(shift => shift.ToDto()).ToList();
    }

    public ShiftDto GetUnfinishedShiftForWorker(int workerId)
    {
        var results = _context.Shifts.Where(sh => sh.WorkerId == workerId && sh.EndTime == null);
        return results.Count() > 0 ? results.First().ToDto() : new ShiftDto(){ WorkerId = workerId };
    }

    public ShiftDto UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = _context.Shifts.Find(id);

        if (savedShift == null)
        {
            return new ShiftDto(){ WorkerId = 0 };
        }

        _context.Entry(savedShift).CurrentValues.SetValues(updatedShift);
        _context.SaveChanges();

        return savedShift.ToDto();
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
