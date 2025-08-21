using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Workers
{
    public static class WorkerMapper
    {
        public static WorkerDto ToDto(this Worker worker) =>
            new WorkerDto() { Id = worker.Id, Name = worker.Name, Shifts = worker.Shifts.Select(sh => sh.ToDto()).ToList()};
    }
}
