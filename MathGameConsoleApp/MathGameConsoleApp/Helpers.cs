using MathGameConsoleApp.Models;
using System.Linq;

namespace MathGameConsoleApp
{
    internal class Helpers
    {

        static List<Game> games = new List<Game>();

        internal static string GetName()
        {
            Console.WriteLine("Please type your name");
            var name = Console.ReadLine();

            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can't be empty");
                name = Console.ReadLine();
            }

            return name;
        }

        internal static GameDifficulty SelectDifficulty()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(@"What difficulty would you like to play on?
E - Easy
M - Medium
H - Hard");
            Console.WriteLine("-----------------------------------------");
            var difficulty = Console.ReadLine();

            switch (difficulty.Trim().ToLower())
            {
                case "e":
                    Console.WriteLine("Easy difficulty selected.");
                    return GameDifficulty.Easy;
                    break;
                case "m":
                    Console.WriteLine("Medium difficulty selected.");
                    return GameDifficulty.Medium;
                    break;
                case "h":
                    Console.WriteLine("Hard difficulty selected.");
                    return GameDifficulty.Hard;
                    break;
                default:
                    Console.WriteLine("Invalid selection. Easy selected by default.");
                    return GameDifficulty.Easy;
                    break;
            }
        }

        internal static string ValidateResult(string result)
        {
            while (string.IsNullOrEmpty(result) || !Int32.TryParse(result, out _))
            {
                Console.WriteLine("Your answer needs to be an integer. Try again.");
                result = Console.ReadLine();
            }

            return result;
        }

        internal static void PrintGames()
        {

            //var gamesToPrint = games.Where(x => x.Date > new DateTime(2025, 06, 20)).OrderByDescending(x => x.Score);

            Console.Clear();
            Console.WriteLine("Games History");
            Console.WriteLine("----------------------");
            foreach (var game in games)
            {
                Console.WriteLine($"{game.Date} - {game.Difficulty} - {game.Type}: {game.Score} points");
            }
            Console.WriteLine("----------------------\n");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadLine();
        }

        internal static void AddToHistory(int gameScore, GameDifficulty difficulty, GameType gameType)
        {
            games.Add(new Game
            {
                Date = DateTime.Now,
                Score = gameScore,
                Type = gameType,
                Difficulty = difficulty
            });
        }

        internal static int[] GetNumbers(GameDifficulty difficulty)
        {
            var random = new Random();

            int upperBound;

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    upperBound = 9;
                    break;
                case GameDifficulty.Medium:
                    upperBound = 99;
                    break;
                case GameDifficulty.Hard:
                    upperBound = 999;
                    break;
                default:
                    upperBound = 9; // Default Easy
                    break;
            }

            int firstNumber = random.Next(0, upperBound);
            int secondNumber = random.Next(0, upperBound);

            var result = new int[2];
            result[0] = firstNumber;
            result[1] = secondNumber;

            return result;
        }

        internal static int[] GetDivisionNumbers(GameDifficulty difficulty)
        {
            var random = new Random();

            int upperBound;

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    upperBound = 99;
                    break;
                case GameDifficulty.Medium:
                    upperBound = 999;
                    break;
                case GameDifficulty.Hard:
                    upperBound = 9999;
                    break;
                default:
                    upperBound = 99; // Default Easy
                    break;
            }

            var firstNumber = random.Next(0, upperBound);
            var secondNumber = random.Next(1, upperBound);

            var result = new int[2];


            while (firstNumber % secondNumber != 0)
            {
                firstNumber = random.Next(0, upperBound);
                secondNumber = random.Next(1, upperBound);
            }

            result[0] = firstNumber;
            result[1] = secondNumber;

            return result;
        }
    }
}
