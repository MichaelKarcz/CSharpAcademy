using FlashCards.Database;
using FlashCards.Controllers;

namespace FlashCards;

public class Program()
{
    public static void Main(string[] args)
    {
        bool initializationSuccessful = GeneralDBHelper.InitializeDatabase();
        Console.WriteLine($"Database initialization result: {initializationSuccessful}\n\n");

        FlashcardController.DisplayFlashcards(FlashcardDBHelper.GetAllFlashcards());

        Console.WriteLine();

        bool flashcardsHaveRecords = FlashcardDBHelper.CheckForRecords();
        bool decksHaveRecords = DeckDBHelper.CheckForRecords();
        bool studySessionsHaveRecords = StudySessionDBHelper.CheckForRecords();

        Console.WriteLine($"Flashcards empty: {!flashcardsHaveRecords}\nDecks empty: {!decksHaveRecords}\nStudy Sessions empty: {!studySessionsHaveRecords}\n");

        Console.WriteLine("Press any key to exit the program.");
        Console.ReadKey();
    }
}
