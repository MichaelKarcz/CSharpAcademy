namespace ShiftLogger.API.Contracts.Shifts
{
    public class ShiftDto
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public required int WorkerId { get; set; }
    }
}
