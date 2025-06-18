using System;
using System.Collections.Generic;
using HabitTracker.Models;
using HabitTrackerLibrary;

namespace HabitTracker
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome to your Habit Tracker.");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\nPress enter to continue to the Main Menu");
            Console.ReadKey();

            Menu.GetUserInput();

        }

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

                

            }

        }

    }
}


