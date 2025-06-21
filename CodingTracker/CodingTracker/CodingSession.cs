using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class CodingSession
    {
        internal int Id { get; set; }
        internal string Date { get; set; } // MM-dd-yyyy
        internal string StartTime { get; set; } // hh:mm tt
        internal string? EndTime { get; set; } // hh:mm tt
        internal string? Duration { get; set; }


        internal CodingSession()
        {
            Date = DateTime.Now.ToString("MM-dd-yyyy", new CultureInfo("en-US"));
            StartTime = DateTime.Now.ToString("hh:mm tt", new CultureInfo("en-US"));
            EndTime = "";
            Duration = "";
        }

        internal void CalculateDuration()
        {
            if (!string.IsNullOrEmpty(EndTime) && !string.IsNullOrEmpty(StartTime))
            {
                DateTime endTimeDT = DateTime.ParseExact(EndTime, "hh:mm tt", new CultureInfo("en-US"));
                DateTime startTimeDT = DateTime.ParseExact(StartTime, "hh:mm tt", new CultureInfo("en-US"));

                Duration = (endTimeDT - startTimeDT).ToString();
            }
            else
            {
                Duration = "";
            }

        }

    }
}
