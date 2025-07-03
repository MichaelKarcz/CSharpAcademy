using DrinksInfo.Engines;
using DrinksInfo.Models;
using DrinksInfo.Services;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DrinksInfo;

public class Program()
{
    public static void Main(string[] args)
    {
        while(true)
        {
            AnsiConsole.Write(new Markup("[grey]Press ctrl+c to exit the program[/]\n\n"));
            List<Category> categories = DrinksService.GetCategories();
            Category cat = IOEngine.SelectACategory(categories);
            AnsiConsole.Clear();

            List<Drink> drinks = DrinksService.GetDrinksByCategory(cat);
            Drink drink = IOEngine.SelectADrink(drinks);
            
            DrinkInfo drinkInfo = DrinksService.GetDrinkInfo(drink);
            IOEngine.DisplayDrinkInfo(drinkInfo);

            AnsiConsole.Write(new Markup("\n[grey]Press any key to return to the main menu.[/]\n"));
            AnsiConsole.Console.Input.ReadKey(false);
            AnsiConsole.Clear();
        }
    }
}



