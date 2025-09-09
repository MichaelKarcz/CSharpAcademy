using ShiftLogger.API.Models.Shifts;

namespace ShiftLogger.API.Interfaces;
public interface IShiftService
{
    public Task<Shift> CreateShiftAsync(Shift shift);
    public Task<List<Shift>> GetAllShiftsForWorkerAsync(int workerId);
    public Task<Shift?> GetShiftByIdAsync(int id);
    public Task<Shift?> GetUnfinishedShiftForWorkerAsync(int workerId);
    public Task<Shift?> UpdateShiftAsync(int id, Shift updatedShift);
    public Task<string> DeleteShiftAsync(int id);
}
