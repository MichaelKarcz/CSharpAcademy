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
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal string? Duration { get; set; }


        internal CodingSession()
        {

        }

        internal void CalculateDuration()
        {

        }

    }
}
