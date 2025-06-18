using System.Data;
using Microsoft.Data.Sqlite;
using HabitTrackerLibrary.Models;

namespace HabitTrackerLibrary
{
    public static class SQLHelper
    {

        public static void CreateDatabaseIfNotExists() 
        {
            string connectionString = @"Data Source=habit-Tracker.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS drinking_water (
                                          Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                          Date TEXT,
                                          Quantity INTEGER
                                          )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static List<Habit> GetAllRecords()
        {
            List<Habit> allEntries = new List<Habit>();


            return allEntries;

        }

        public static void InsertSingleRecord(Habit habit)
        {

        }

        public static bool DeleteRecord(int id)
        {
            bool deleteSuccessful = false;



            return deleteSuccessful;
        }

        public static bool UpdateRecord(int id, Habit habit)
        {
            bool updateSuccessful = false;

            return updateSuccessful;
        }

    }
}
