using Microsoft.EntityFrameworkCore;
using ShiftLogger.API.Contracts.Shifts;
using ShiftLogger.API.Contracts.Worker;

namespace ShiftLogger.API.Data;
public class ShiftLoggerDbContext : DbContext
{
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }

    public ShiftLoggerDbContext()
    {

    }

    public ShiftLoggerDbContext(DbContextOptions<ShiftLoggerDbContext> options) : base(options)
    {

    }
}
