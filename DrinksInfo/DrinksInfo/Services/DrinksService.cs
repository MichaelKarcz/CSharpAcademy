using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;
using DrinksInfo.Models;

namespace DrinksInfo.Services
{
    internal static class DrinksService
    {
        internal static List<Category> GetCategories()
        {
            RestClient Client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            RestRequest request = new RestRequest("list.php?c=list");
            var response = Client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                List<Category> categories = serialize.CategoriesList;

                return categories;
            }
            else return new List<Category>();
        }

        internal static List<Drink> GetDrinksByCategory(Category category)
        {
            string categoryName = category.ToString().Replace(" ", "_");
            RestClient Client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            RestRequest request = new RestRequest("filter.php?c=" + categoryName);
            var response = Client.ExecuteAsync(request);
            
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                List<Drink> categories = serialize.DrinksList;

                return categories;
            }
            else return new List<Drink>();
        }

        internal static DrinkInfo GetDrinkInfo(Drink drink)
        {
            RestClient Client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            RestRequest request = new RestRequest("lookup.php?i=" + drink.GetId().ToString());
            var response = Client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkInfos>(rawResponse);

                List<DrinkInfo> drinkInfoList = serialize.DrinkInfoList;

                return drinkInfoList[0];
            }
            else return new DrinkInfo();
        }
    }
}
