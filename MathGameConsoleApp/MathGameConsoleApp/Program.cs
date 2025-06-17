using System.ComponentModel.DataAnnotations;

var date = DateTime.Now;
var name = GetName();

Menu(name);


string GetName()
{
    Console.WriteLine("Please type your name");
    var name = Console.ReadLine();
    return name;
}


void Menu(string name)
{
    Console.WriteLine("-----------------------------------------");
    Console.WriteLine($"Hello {name}. It's {date.DayOfWeek}. This is your math game.\n");
    Console.WriteLine($@"What game would you like to play today? Choose from the options below:
A - Addition
S - Subtraction
M - Multiplication
D - Division
Q - Quit the program");
    Console.WriteLine("-----------------------------------------");

    var gameSelected = Console.ReadLine();

    switch (gameSelected.Trim().ToLower())
    {
        case "a":
            AdditionGame("Addition game");
            break;
        case "s":
            SubtractionGame("Subtraction game");
            break;
        case "m":
            MultiplicationGame("Multiplication game");
            break;
        case "d":
            DivisionGame("Division game");
            break;
        case "q":
            Console.WriteLine("Goodbye.");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid input. Goodbye.");
            Environment.Exit(1);
            break;

    }



    Console.ReadLine();
}


void AdditionGame(string message)
{
    var random = new Random();


    int firstNumber;
    int secondNumber;

    int score = 0;

    for (int i=0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine(message);
        firstNumber = random.Next(1, 9);
        secondNumber = random.Next(1, 9);

        Console.WriteLine($"{firstNumber} + {secondNumber}");
        var result = Console.ReadLine();

        if (int.Parse(result) == firstNumber + secondNumber)
        {
            Console.WriteLine("Your answer was correct! Type any key for the next question.");
            score++;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Your answer was incorrect. Type any key for the next question.");
            Console.ReadLine();
        }

        if (i == 4) Console.WriteLine($"Game over! Your score was: {score}");

    }
    
}


void SubtractionGame(string message)
{
    var random = new Random();


    int firstNumber;
    int secondNumber;

    int score = 0;

    for (int i = 0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine(message);
        firstNumber = random.Next(1, 9);
        secondNumber = random.Next(1, 9);

        Console.WriteLine($"{firstNumber} - {secondNumber}");
        var result = Console.ReadLine();

        if (int.Parse(result) == firstNumber - secondNumber)
        {
            Console.WriteLine("Your answer was correct! Type any key for the next question.");
            score++;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Your answer was incorrect. Type any key for the next question.");
            Console.ReadLine();
        }

        if (i == 4) Console.WriteLine($"Game over! Your score was: {score}");

    }

}


void MultiplicationGame(string message)
{
    var random = new Random();


    int firstNumber;
    int secondNumber;

    int score = 0;

    for (int i = 0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine(message);
        firstNumber = random.Next(1, 9);
        secondNumber = random.Next(1, 9);

        Console.WriteLine($"{firstNumber} * {secondNumber}");
        var result = Console.ReadLine();

        if (int.Parse(result) == firstNumber * secondNumber)
        {
            Console.WriteLine("Your answer was correct! Type any key for the next question.");
            score++;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Your answer was incorrect. Type any key for the next question.");
            Console.ReadLine();
        }

        if (i == 4) Console.WriteLine($"Game over! Your score was: {score}");

    }

}


void DivisionGame(string message)
{
    int firstNumber;
    int secondNumber;

    int score = 0;

    for (int i = 0; i < 5; i++)
    {
        Console.Clear();
        Console.WriteLine(message);
        var divisionNumbers = GetDivisionNumbers();
        firstNumber = divisionNumbers[0];
        secondNumber = divisionNumbers[1];

        Console.WriteLine($"{firstNumber} / {secondNumber}");
        var result = Console.ReadLine();

        if (int.Parse(result) == firstNumber / secondNumber)
        {
            Console.WriteLine("Your answer was correct! Type any key for the next question.");
            score++;
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Your answer was incorrect. Type any key for the next question.");
            Console.ReadLine();
        }

        if (i == 4) Console.WriteLine($"Game over! Your score was: {score}");

    }

}

int[] GetDivisionNumbers()
{
    var random = new Random();
    var firstNumber = random.Next(0, 99);
    var secondNumber = random.Next(1, 99);

    var result = new int[2];

    
    while (firstNumber % secondNumber != 0)
    {
        firstNumber = random.Next(0, 99);
        secondNumber = random.Next(1, 99);
    }

    result[0] = firstNumber;
    result[1] = secondNumber;

    return result;
}

