namespace ShiftLogger.API.Contracts.Shifts
{
    public static class ShiftMapper
    {
        public static ShiftDto ToDto(this Shift shift) =>
            new ShiftDto() { StartTime = shift.StartTime, EndTime = shift.EndTime, WorkerId = shift.WorkerId };
    }
}
