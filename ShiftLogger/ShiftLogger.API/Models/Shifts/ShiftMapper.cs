using ShiftLogger.Contracts.Responses.Shifts;

namespace ShiftLogger.API.Models.Shifts;

public static class ShiftMapper
{
    public static ShiftResponse ToResponse(this Shift shift) =>
        new ShiftResponse() { Id = shift.Id, StartTime = shift.StartTime, EndTime = shift.EndTime};
}
