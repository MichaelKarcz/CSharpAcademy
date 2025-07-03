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
        public int idDrink { get; set; }
        public string strDrink { get; set; }

        public int GetId()
        {
            return idDrink;
        }
        public override string ToString()
        {
            return strDrink;
        }
    }

    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinksList;
    }
}
