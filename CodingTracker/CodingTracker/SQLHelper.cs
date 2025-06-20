using System.Configuration;
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
                var connection2 = new SqliteConnection(CONNECTION_STRING);

                string commandText = $@"CREATE TABLE IF NOT EXISTS {TABLENAME} (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              StartTime TEXT,
                                              EndTime TEXT,
                                              Duration TEXT
                                              )";

                connection2.Execute(commandText);

                /*
                using (var connection = new SqliteConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();

                    tableCmd.CommandText = $@"CREATE TABLE IF NOT EXISTS {TABLENAME} (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              StartTime TEXT,
                                              EndTime TEXT,
                                              Duration TEXT
                                              )";

                    tableCmd.ExecuteNonQuery();

                    connection.Close();
                }
                */
            }
            catch (SqliteException e)
            {
                Console.WriteLine($"\n\nThere was an error trying to instantiate the database. Error message: {e.Message}\n\n");
            }
        }

    }
}
