using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftLogger.API.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string StartTime { get; set; } = DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
        public string? EndTime { get; set; }
        public required int WorkerId { get; set; }
        public virtual required Worker Worker { get; set; }

    }
}
