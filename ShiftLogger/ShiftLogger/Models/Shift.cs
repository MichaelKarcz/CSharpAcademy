using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftLogger.Models
{
    internal class Shift
    {
        internal int Id { get; set; }
        internal string StartTime { get; set; } = DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
        internal string? EndTime { get; set; }
        internal required int WorkerId { get; set; }

        internal Shift()
        {

        }
    }
}
