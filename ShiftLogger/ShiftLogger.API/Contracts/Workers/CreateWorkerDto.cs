using ShiftLogger.API.Contracts.Shifts;

namespace ShiftLogger.API.Contracts.Workers
{
    public class CreateWorkerDto
    {
        public string Name { get; set; } = string.Empty;

        public CreateWorkerDto()
        {

        }

        public CreateWorkerDto(string name)
        {
            Name = name;
        }
    }
}
