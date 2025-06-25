using Microsoft.Data.SqlClient;
using Dapper;
using FlashCards.Models;

namespace FlashCards.Database
{
    public class FlashcardDBHelper
    {

        private string _connectionString {  get; set; }


        public FlashcardDBHelper(string dataSource, string username, string password, string initialCatalog)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource, //"<your_server.database.windows.net>",
                UserID = username, //"<your_username>",
                Password = password, //"<password>",
                InitialCatalog = initialCatalog //"<your_database>"
            };

            _connectionString = builder.ConnectionString;
        }

        public List<object> PerformReadOperation(string sqlCommand, string parameters)
        {
            List<object> returnedRecords= new List<object>();

            SqlConnection conn = new SqlConnection(_connectionString);



            return returnedRecords;
        }

        private SqlConnection CreateSQLConnection(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                return conn;
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"An error occurred creating the SQL Connection. Error message: {ex.ToString()}");
                throw;
            }
        }

    }
}
