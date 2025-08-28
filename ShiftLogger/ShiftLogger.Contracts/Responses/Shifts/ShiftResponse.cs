namespace ShiftLogger.Contracts.Responses.Shifts;

public class ShiftResponse
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }

    public override string ToString()
    {
        string endTimeString = EndTime.HasValue ? EndTime.Value.ToString("MM-dd-yyyy hh:mm tt") : "-----------------";
        return $"Id: {Id} | Start time: {StartTime.ToString("MM-dd-yyyy hh:mm tt")} | End time: {endTimeString}";
    }
}
