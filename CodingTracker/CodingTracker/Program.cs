using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;

namespace CodingTracker
{
    public class Program()
    {
        public static void Main(string[] args)
        {
            SQLHelper.CreateDatabaseIfNotExists();

            string sAttr = ConfigurationManager.AppSettings.Get("connectionString");

            Console.WriteLine($"The value of connectionString is {sAttr}");

            var test = DateTime.Now.ToString("hh:mm tt");

            DateTime testParse = DateTime.ParseExact("04:22 PM", "hh:mm tt", new CultureInfo("en-US"));

            var timeOne = DateTime.ParseExact("05:30 PM", "hh:mm tt", new CultureInfo("en-US"));
            var timeTwo = DateTime.ParseExact("08:30 AM", "hh:mm tt", new CultureInfo("en-US"));

            var timeDifference = timeTwo - timeOne;

            Console.WriteLine($"\nDateTime value: {test}");
            Console.WriteLine($"\nDateTime parsed from string: {testParse}");
            Console.WriteLine($"\nTime Difference between {timeTwo} and {timeOne} = {timeDifference}");

            Console.ReadKey();

        }
    }
}