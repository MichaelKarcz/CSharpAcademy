using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Workers
{
    public class WorkerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Shift>? Shifts { get; set; }
    }
}
