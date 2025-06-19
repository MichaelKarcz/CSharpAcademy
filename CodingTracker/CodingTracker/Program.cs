using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker
{
    public class Program()
    {
        public static void Main(string[] args)
        {

            string sAttr = ConfigurationManager.AppSettings.Get("connectionString");

            Console.WriteLine($"The value of connectionString is {sAttr}");

            Console.ReadKey();

        }
    }
}