using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo.Models
{
    public class Drink
    {
        [JsonProperty("idDrink")] int IdDrink;
        [JsonProperty("strDrink")] string? StrDrink;

        public int GetId()
        {
            return IdDrink;
        }
        public override string ToString()
        {
            return StrDrink;
        }
    }

    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinksList;
    }
}
