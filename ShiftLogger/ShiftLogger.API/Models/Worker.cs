namespace ShiftLogger.API.Models
{
    public class Worker
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        public Worker()
        {
            Name = string.Empty;
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
}
