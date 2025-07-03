using DrinksInfo.Models;
using Newtonsoft.Json.Serialization;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo.Engines
{
    internal static class IOEngine
    {
        internal static Category SelectACategory(List<Category> categories)
        {


            Category category = AnsiConsole.Prompt(
                new SelectionPrompt<Category>()
                .Title("Select a category:")
                .PageSize(10)
                .MoreChoicesText("[grey]Use the up and down arrow keys to reveal more categories[/]")
                .AddChoices(categories.ToArray())
                );

            return category;
        }

        internal static Drink SelectADrink(List<Drink> drinks)
        {
            Drink drink = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                .Title("Select a drink:")
                .PageSize(10)
                .MoreChoicesText("[grey]Use the up and down arrow keys to reveal more categories[/]")
                .AddChoices(drinks.ToArray())
                );

            return drink;

        }

        internal static void DisplayDrinkInfo(DrinkInfo drinkInfo)
        {
            foreach (PropertyInfo prop in drinkInfo.GetType().GetProperties())
            {
                string propertyName = prop.Name.Contains("str") ? prop.Name.Substring(3) : prop.Name;

                if (!string.IsNullOrEmpty(prop.GetValue(drinkInfo)?.ToString()))
                {
                    Console.WriteLine($"{propertyName} : {prop.GetValue(drinkInfo)?.ToString()}");
                }
            }
        }


    }
}
