using Dapper;
using FlashCards.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Database
{
    internal static class DeckDBHelper
    {
        private static string CONNECTION_STRING = ConfigurationManager.AppSettings.Get("flashcardsConnectionString");


        internal static bool CheckForRecords()
        {
            SqlConnection conn = GeneralDBHelper.CreateSQLConnection(CONNECTION_STRING);
            string sql = @"SELECT TOP (1) * FROM Decks";

            List<DeckDTO> returnedRecords = conn.Query<DeckDTO>(sql).ToList();

            return returnedRecords.Count > 0;
        }
    }
}
