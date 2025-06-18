using System.Globalization;
using HabitTrackerLibrary;
using HabitTrackerLibrary.Models;


namespace HabitTracker
{
    internal static class Menu
    {
        internal static void GetUserInput()
        {
            Console.Clear();

            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("What would you like to do? ");
                Console.WriteLine("0 - Close Application");
                Console.WriteLine("1 - View All Records");
                Console.WriteLine("2 - Insert Record");
                Console.WriteLine("3 - Delete Record");
                Console.WriteLine("2 - Update Record");
                Console.WriteLine("-------------------------\n");

                string commandInput = Console.ReadLine();

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        break;
                    case "1":
                        ShowAllRecords();
                        break;
                    case "2":
                        InsertRecord();
                        break;
                    case "3":
                        DeleteRecord();
                        break;
                    case "4":
                        UpdateRecord();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please type a number from 0 to 4.\n");
                        break;
                }

            }
            
        }

        internal static void ShowAllRecords()
        {
            List<Habit> allEntries = SQLHelper.GetAllRecords();

            Console.WriteLine("-------------------------");
            foreach (Habit entry in allEntries)
            {
                Console.WriteLine($"{entry.Id} | {entry.Date} | {entry.Quantity}");
            }
            Console.WriteLine("-------------------------");

        }

        internal static void InsertRecord()
        {
            Habit habit = new Habit();

            Console.Clear();
            Console.WriteLine("Please provide the information for the habit to be tracked.");

            habit.Date = GetDateInput();
            habit.Quantity = GetNumberInput("\nEnter the quantity by which you're measuring this habit. ");

            SQLHelper.InsertSingleRecord(habit);
        }

        internal static void DeleteRecord()
        {
            ShowAllRecords();

            int recordID = GetNumberInput("\nPlease select a record ID to delete.");

            bool deleteSuccessful = SQLHelper.DeleteRecord(recordID);

            if (!deleteSuccessful)
            {
                
            }
            Console.WriteLine($"\nRecord with ID = {recordID} has been deleted.");
        }

        internal static void UpdateRecord()
        {
            ShowAllRecords();

            int recordID = GetNumberInput("\nPlease select a record ID to update.");


            
        }

        internal static string GetDateInput()
        {

            Console.WriteLine("\nEnter the date in the format dd-mm-yy. Type 0 to enter today's date.");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") return DateTime.Now.ToShortDateString();

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. Format must be dd-mm-yy. Try again, or type 0 to enter today's date.");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            while (string.IsNullOrEmpty(numberInput) || !int.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("Invalid number format. Please enter a positive number without decimals.");
                numberInput = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }
    }
}
