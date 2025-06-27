using Dapper;
using FlashCards.DTOs;
using FlashCards.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Database
{
    internal static class StudySessionDBHelper
    {
        private static string CONNECTION_STRING = ConfigurationManager.AppSettings.Get("flashcardsConnectionString");

        internal static bool CheckForRecords()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT TOP (1) * FROM StudySessions";
            List<StudySessionDTO> returnedRecords = conn.Query<StudySessionDTO>(sql).ToList();

            return returnedRecords.Count > 0;
        }

        internal static List<StudySession> GetAllStudySessions()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT * FROM StudySessions";
            List<StudySession> returnedRecords = conn.Query<StudySession>(sql).ToList();

            return returnedRecords;
        }

        internal static bool InsertStudySession(StudySession studySession)
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"INSERT INTO StudySessions (Score, SessionDate, DeckId) VALUES (@Score, @SessionDate, @DeckId)";
            int rowsAffected = conn.Execute(sql, new { Score = studySession.Score, SessionDate = studySession.SessionDate, DeckId = studySession.StudyDeck});

            return  rowsAffected > 0;
        }

        internal static bool UpdateStudySessionById(int id, StudySession studySession)
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"UPDATE StudySessions
                            SET Score = @Score, SessionDate = @SessionDate, DeckId = @DeckId
                            WHERE Id = @Id";
            int rowsAffected = conn.Execute(sql, new {Score = studySession.Score, SessionDate = studySession.SessionDate, DeckId = studySession.StudyDeck, Id = id });

            return rowsAffected > 0;
        }

        internal static bool DeleteStudySessionById(int id)
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"DELETE * FROM StudySessions WHERE Id = @Id";
            int rowsAffected = conn.Execute(sql, new { Id = id });

            return rowsAffected > 0;
        }

        internal static bool DeleteAllStudySessionsOfDeck(int deckId)
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"DELETE * FROM StudySessions WHERE DeckId = @DeckId";
            int rowsAffected = conn.Execute(sql, new { DeckId = deckId });

            return rowsAffected > 0;
        }
    }
}
