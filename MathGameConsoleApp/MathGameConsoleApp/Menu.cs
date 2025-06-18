namespace MathGameConsoleApp
{
    internal class Menu
    {
        GameEngine gameEngine = new GameEngine();

        internal void ShowMenu(string name, DateTime date)
        {
            Console.WriteLine($"Hello {name}. It's {date.DayOfWeek}. This is your math game. Press any key to continue.\n");
            Console.ReadLine();

            bool isGameOn = true;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine(@"What game would you like to play today? Choose from the options below:
V - View Previous Games
A - Addition
S - Subtraction
M - Multiplication
D - Division
Q - Quit the program");
                Console.WriteLine("-----------------------------------------");

                var gameSelected = Console.ReadLine();

                switch (gameSelected.Trim().ToLower())
                {
                    case "v":
                        Helpers.PrintGames();
                        break;
                    case "a":
                        gameEngine.AdditionGame("Addition game");
                        break;
                    case "s":
                        gameEngine.SubtractionGame("Subtraction game");
                        break;
                    case "m":
                        gameEngine.MultiplicationGame("Multiplication game");
                        break;
                    case "d":
                        gameEngine.DivisionGame("Division game");
                        break;
                    case "q":
                        Console.WriteLine("Goodbye.");
                        isGameOn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Goodbye.");
                        isGameOn = false;
                        break;

                }
            }
            while (isGameOn);

            Environment.Exit(0);
            Console.ReadLine();
        }
    }
}
