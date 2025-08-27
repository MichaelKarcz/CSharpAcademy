namespace ShiftLogger.Contracts.Requests.Shifts;

public class CreateShiftRequest
{
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public required int WorkerId { get; set; }
}
