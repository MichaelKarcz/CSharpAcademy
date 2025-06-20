using System.Configuration;
using System.Globalization;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal static class SQLHelper
    {
        private static readonly string CONNECTION_STRING = ConfigurationManager.AppSettings.Get("connectionString");
        private static readonly string TABLENAME = "codingSessions";

        public static void CreateDatabaseIfNotExists()
        {
            try
            {
                var connection = new SqliteConnection(CONNECTION_STRING);

                string commandText = $@"CREATE TABLE IF NOT EXISTS {TABLENAME} (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              StartTime TEXT,
                                              EndTime TEXT,
                                              Duration TEXT
                                              )";

                connection.Execute(commandText);
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"\n\nThere was an error trying to instantiate the database. Error message: {e.Message}\n\n");
            }
        }

        public static List<CodingSession> GetAllRecords()
        {
            List<CodingSession> allEntries = new List<CodingSession>();

            try
            {
                using (var connection = new SqliteConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();

                    tableCmd.CommandText = $@"SELECT * FROM {TABLENAME}";

                    SqliteDataReader reader = tableCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            allEntries.Add(
                                new CodingSession
                                {
                                    Id = reader.GetInt32(0),
                                    Date = DateTime.ParseExact(reader.GetString(1), "mm-dd-yyyy", new CultureInfo("en-US")),
                                    StartTime = reader.GetString(2),
                                    EndTime = reader.GetString(3),
                                    Duration = reader.GetString(4)
                                });
                        }
                    }
                    else
                    {
                        return new List<CodingSession>();
                    }

                    connection.Close();
                }

            }
            catch (SqliteException e)
            {
                Console.WriteLine($"\n\nThere was an error trying to retrieve the database records. Error message: {e.Message}\n\n");
                return new List<CodingSession>();
            }

            return allEntries;
        }

        public static bool InsertSingleRecord(CodingSession habit)
        {
            string commandText = $@"INSERT INTO {TABLENAME} (Date, StartTime, EndTime, Duration)
                                 VALUES ('{habit.Date}', '{habit.StartTime}', '{habit.EndTime}', '{habit.Duration}');";

            return PerformCUDOperation(commandText);
        }

        public static bool DeleteRecord(int id)
        {
            string commandText = $@"DELETE FROM {TABLENAME}
                                 WHERE Id={id};";

            return PerformCUDOperation(commandText);
        }

        public static bool UpdateRecord(int id, CodingSession habit)
        {
            string commandText = $@"UPDATE {TABLENAME}
                                 SET Date='{habit.Date}', StartTime='{habit.StartTime}', EndTime='{habit.EndTime}', Duration='{habit.Duration}'
                                 WHERE Id={id};";

            return PerformCUDOperation(commandText);
        }

        private static bool PerformCUDOperation(string commandText)
        {
            bool commandSuccessful = false;

            try
            {
                using (var connection = new SqliteConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = commandText;

                    int rowsAffected = tableCmd.ExecuteNonQuery();

                    if (rowsAffected > 0) commandSuccessful = true;

                    connection.Close();
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"\n\n{e.Message}\n\n");
                commandSuccessful = false;
            }

            return commandSuccessful;
        }

    }
}
