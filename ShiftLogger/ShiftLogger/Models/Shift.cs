using Newtonsoft.Json;

namespace ShiftLogger.Models;

internal class Shift
{
    [JsonProperty("id")] internal int Id { get; set; }
    [JsonProperty("starttime")] internal DateTime StartTime { get; set; } = DateTime.Now;
    [JsonProperty("endtime")] internal DateTime? EndTime { get; set; }
    [JsonProperty("workerid")] internal required int WorkerId { get; set; }

    internal Shift()
    {

    }
}
