using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class CodingSession
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }
        internal string StartTime { get; set; }
        internal string EndTime { get; set; }
        internal string? Duration { get; set; }


        internal CodingSession()
        {

        }

        internal void CalculateDuration()
        {

        }

    }
}
