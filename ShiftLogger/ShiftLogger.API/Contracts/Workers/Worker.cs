using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Workers;
public class Worker
{
    public int Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Shift>? Shifts { get; set; }

    public Worker()
    {
        
    }

    public Worker(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}
