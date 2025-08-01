using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Worker
{
    public static class WorkerMapper
    {
        public static WorkerDto ToDto(this Worker worker) =>
            new WorkerDto() { Username = worker.Username, Name = worker.Name, Shifts = worker.Shifts};
    }
}
