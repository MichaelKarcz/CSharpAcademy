using System.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace FlashCards.Database
{
    internal static class GeneralDBHelper
    {

        public static bool InitializeDatabase()
        {
            bool initializationSuccessful = false;

            try
            {
                string connectionString = ConfigurationManager.AppSettings.Get("masterConnectionString");
                using SqlConnection connection = CreateSQLConnection(connectionString);

                string sql = @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Flashcards')
                                BEGIN
                                    CREATE DATABASE [Flashcards]
                                END
                               GO
                                    USE [Flashcards]
                               GO
                               IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Decks' AND xtype='U')
                                BEGIN
                                    CREATE TABLE Decks (
                                        Id INT PRIMARY KEY IDENTITY (1,1),
                                        Name VARCHAR(255) NOT NULL
                                    );
                                END
                               GO
                               IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Flashcards' AND xtype='U')
                                BEGIN
                                    CREATE TABLE Flashcards (
                                        Id INT PRIMARY KEY IDENTITY (1,1),
                                        Front NVARCHAR(255) NOT NULL,
                                        Back NVARCHAR(255) NOT NULL,
                                        DeckId INT NOT NULL,
                                        FOREIGN KEY (DeckId) REFERENCES Decks(Id)
                                    );
                                END
                               GO
                               IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StudySessions' AND xtype='U')
                                BEGIN
                                    CREATE TABLE StudySessions (
                                        Id INT PRIMARY KEY IDENTITY (1,1),
                                        Score INT NOT NULL,
                                        SessionDate DATETIME NOT NULL,
                                        DeckId INT NOT NULL,
                                        FOREIGN KEY (DeckId) REFERENCES Decks(Id)
                                    );
                                END
                               GO";

                connection.Execute(sql);

                initializationSuccessful = true;

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"\n\nAn error has occurred while initializing the database. Error message: {ex.Message}\n\n");
                initializationSuccessful = false;
            }

            return initializationSuccessful;
        }

        
        private static SqlConnection CreateSQLConnection(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred creating the SQL Connection. Error message: {ex.ToString()}");
                throw;
            }
        }
    }
}
