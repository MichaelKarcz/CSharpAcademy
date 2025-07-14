using Newtonsoft.Json;

namespace ShiftLogger.Models;
internal class Worker
{
    internal int Id { get; set; }
    internal string Name { get; set; } = string.Empty;
    
    internal Worker()
    {

    }
}

internal class Workers
{
    [JsonProperty("workers")]
    internal List<Worker> WorkersList;
}
