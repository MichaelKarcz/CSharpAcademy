using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo.Models
{
    public class Category
    {
        [JsonProperty("strCategory")] string? StrCategory;
        public override string ToString()
        {
            return StrCategory ?? "";
        }
    }
    
    

    public class Categories
    {
        [JsonProperty("drinks")]
        public List<Category> CategoriesList;
    }

}
