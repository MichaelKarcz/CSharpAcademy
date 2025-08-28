using ShiftLogger.Contracts.Responses.Shifts;

namespace ShiftLogger.Contracts.Responses.Workers;

public class WorkerResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ShiftResponse>? Shifts { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
