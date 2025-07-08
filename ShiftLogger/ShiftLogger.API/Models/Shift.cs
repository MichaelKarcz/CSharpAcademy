namespace ShiftLogger.API.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string? EndTime { get; set; }
        public int WorkerId { get; set; }
    }
}
