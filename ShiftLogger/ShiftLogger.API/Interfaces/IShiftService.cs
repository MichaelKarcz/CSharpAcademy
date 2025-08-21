using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Interfaces;
public interface IShiftService
{
    public Shift CreateShift(Shift shift);
    public List<Shift> GetAllShiftsForWorker(int workerId);
    public Shift? GetShiftById(int id);
    public Shift? GetUnfinishedShiftForWorker(int workerId);
    public Shift? UpdateShift(int id, Shift updatedShift);
    public string DeleteShift(int id);
}
