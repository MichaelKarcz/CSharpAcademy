namespace ShiftLogger.API.Contracts.Workers
{
    public class UpdateWorkerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public UpdateWorkerDto()
        {

        }

        public UpdateWorkerDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
