namespace ShiftLogger.Contracts.Requests.Workers;

public class UpdateWorkerRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
