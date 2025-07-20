using Newtonsoft.Json;

namespace ShiftLogger.Models;

internal class Worker
{
    [JsonProperty("id")] internal int Id { get; set; }
    [JsonProperty("name")] internal string Name { get; set; } = string.Empty;
    [JsonProperty("shifts")] internal List<Shift>? Shifts { get; set; }
    
    internal Worker()
    {

    }
}