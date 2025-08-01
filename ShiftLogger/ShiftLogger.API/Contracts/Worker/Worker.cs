using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Worker;
public class Worker
{
    public int Id {  get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Shift>? Shifts { get; set; }

    public Worker()
    {
        
    }

    public Worker(string name, string username)
    {
        Name = name;
        Username = username;
    }

    public override string ToString()
    {
        return Name;
    }
}
