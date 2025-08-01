using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Worker
{
    public class WorkerDto
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Shift>? Shifts { get; set; }
    }
}
