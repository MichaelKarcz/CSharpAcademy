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


        internal static bool CheckForRecords()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT TOP (1) * FROM Flashcards";

            List<FlashcardDTO> returnedRecords = conn.Query<FlashcardDTO>(sql).ToList();

            return returnedRecords.Count > 0;
        }

        internal static List<FlashcardDTO> GetAllFlashcards()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT * FROM Flashcards";
            List<FlashcardDTO> returnedRecords = conn.Query<FlashcardDTO>(sql).ToList();

            return returnedRecords;
        }

        internal static bool InsertFlashcard(Flashcard flashcard)
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"INSERT INTO Flashcards (Front, Back, DeckId) 
                            VALUES (@Front, @Back, @DeckId)";
            int rowsAffected = conn.Execute(sql, new { Front = flashcard.Front, Back = flashcard.Back, DeckId = flashcard.DeckId });

            return rowsAffected > 0;
        }
    }
}
