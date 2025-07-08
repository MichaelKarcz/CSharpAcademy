using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Models;
using System.Configuration;

namespace ShiftLogger.API.Data
{
    public class ShiftLoggerDbContext : DbContext
    {
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public ShiftLoggerDbContext()
        {

        }
    }
}
