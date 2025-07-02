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
        List<Category> categories = DrinksService.GetCategories();

        Category cat = IOEngine.SelectACategory(categories);

        List<Drink> drinks = DrinksService.GetDrinksByCategory(cat);
        Drink drink = IOEngine.SelectADrink(drinks);
        DrinkInfo drinkInfo = DrinksService.GetDrinkInfo(drink);
        IOEngine.DisplayDrinkInfo(drinkInfo);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();


    }


}



