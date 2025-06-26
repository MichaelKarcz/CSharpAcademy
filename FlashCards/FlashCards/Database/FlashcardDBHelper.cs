using Microsoft.Data.SqlClient;
using Dapper;
using FlashCards.Models;
using System.Configuration;
using FlashCards.DTOs;

namespace FlashCards.Database
{
    internal static class FlashcardDBHelper
    {

        private static string CONNECTION_STRING = ConfigurationManager.AppSettings.Get("flashcardsConnectionString");

        internal static List<FlashcardDTO> GetAllFlashcards()
        {
            List<FlashcardDTO> returnedRecords = new List<FlashcardDTO>();

            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT * FROM Flashcards";

            returnedRecords = conn.Query<FlashcardDTO>(sql).ToList();


            return returnedRecords;
        }

        internal static bool CheckForRecords()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT TOP (1) * FROM Flashcards";

            List<FlashcardDTO> returnedRecords = conn.Query<FlashcardDTO>(sql).ToList();

            return returnedRecords.Count > 0;
        }
    }
}
