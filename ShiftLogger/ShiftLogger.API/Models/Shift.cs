namespace ShiftLogger.API.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public required int WorkerId { get; set; }
}


