using FlashCards.Database;

namespace FlashCards;

public class Program()
{
    public static void Main(string[] args)
    {
        bool initializationSuccessful = GeneralDBHelper.InitializeDatabase();

        Console.WriteLine($"Database initialization result: {initializationSuccessful}");

        Console.WriteLine("Press any key to exit the program.");
        Console.ReadKey();
    }
}
