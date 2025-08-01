using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Interfaces;
public interface IShiftService
{
    public ShiftDto CreateShift(Shift shift);
    public List<ShiftDto> GetAllShiftsForWorker(int workerId);
    public ShiftDto GetUnfinishedShiftForWorker(int workerId);
    public ShiftDto UpdateShift(int id, Shift updatedShift);
    public string DeleteShift(int id);
}
