namespace ShiftLogger.Contracts.Requests.Shifts
{
    public class UpdateShiftRequest
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
    }
}
