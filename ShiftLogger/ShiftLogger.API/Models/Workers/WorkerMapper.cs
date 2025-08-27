using ShiftLogger.API.Models.Shifts;
using ShiftLogger.API.Models.Workers;
using ShiftLogger.Contracts.Responses.Workers;

namespace ShiftLogger.API.Models.Workers;

public static class WorkerMapper
{
    public static WorkerResponse ToResponse(this Worker worker) =>
        new WorkerResponse() { Id = worker.Id, Name = worker.Name, Shifts = worker.Shifts.Select(sh => sh.ToResponse()).ToList()};
}
