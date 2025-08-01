using Newtonsoft.Json;

namespace ShiftLogger.Models;

internal class Worker
{
    [JsonProperty("name")] internal string Name { get; set; } = string.Empty;
    [JsonProperty("username")] internal string Username { get; set; } = string.Empty;
    [JsonProperty("shifts")] internal List<Shift>? Shifts { get; set; }
    
    internal Worker()
    {

    }
}