using System.ComponentModel.DataAnnotations.Schema;
using ShiftLogger.API.Models.Workers;

namespace ShiftLogger.API.Models.Shifts;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    [ForeignKey("Worker")]
    public required int WorkerId { get; set; }
    public required Worker Worker { get; set; }
}


