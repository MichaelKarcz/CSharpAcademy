using DrinksInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    internal static class Validation
    {
        internal static bool ValidateCategory(List<Category> categories, string input)
        {
            Category category = categories.First(c => string.Equals(c.ToString().Trim().ToLower(), input.Trim().ToLower()));

            if (category != null) return true;
            else return false;
        }

        internal static bool ValidateDrink(List<Drink> drinks, string input)
        {
            Drink drink = drinks.First(d => string.Equals(d.ToString().Trim().ToLower(), input.Trim().ToLower()));

            if (drink != null) return true;
            else return false;
        }


    }
}
