namespace ShiftLogger.API.Contracts.Shifts
{
    public class UpdateShiftDto
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
    }
}
