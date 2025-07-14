namespace ShiftLogger.Models;
internal class Shift
{
    internal int Id { get; set; }
    internal DateTime StartTime { get; set; } = DateTime.Now;
    internal DateTime? EndTime { get; set; }
    internal required int WorkerId { get; set; }

    internal Shift()
    {

    }
}
