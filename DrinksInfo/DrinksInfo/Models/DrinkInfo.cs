using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo.Models
{
    public class DrinkInfo
    {
        [JsonProperty("strDrink")] string? StrDrink;
        [JsonProperty("strDrinkAlternate")] string? StrDrinkAlternate;
        [JsonProperty("strTags")] string? StrTags;
        [JsonProperty("strVideo")] string? StrVideo;
        [JsonProperty("strCategory")] string? StrCategory;
        [JsonProperty("strIBA")] string? StrIBA;
        [JsonProperty("strAlcoholic")] string? StrAlcoholic;
        [JsonProperty("strGlass")] string? StrGlass;
        [JsonProperty("strInstructions")] string? StrInstructions;
        [JsonProperty("strInstructionsES")] string? StrInstructionsES;
        [JsonProperty("strInstructionsDE")] string? StrInstructionsDE;
        [JsonProperty("strInstructionsFR")] string? StrInstructionsFR;
        [JsonProperty("strInstructionsIT")] string? StrInstructionsIT;
        [JsonProperty("strInstructionsZH-HANS")] string? StrInstructionsZHHANS;
        [JsonProperty("strInstructionsZH-HANT")] string? StrInstructionsZHHANT;
        [JsonProperty("strDrinkThumb")] Uri? StrDrinkThumb;
        [JsonProperty("strIngredient1")] string? StrIngredient1;
        [JsonProperty("strIngredient2")] string? StrIngredient2;
        [JsonProperty("strIngredient3")] string? StrIngredient3;
        [JsonProperty("strIngredient4")] string? StrIngredient4;
        [JsonProperty("strIngredient5")] string? StrIngredient5;
        [JsonProperty("strIngredient6")] string? StrIngredient6;
        [JsonProperty("strIngredient7")] string? StrIngredient7;
        [JsonProperty("strIngredient8")] string? StrIngredient8;
        [JsonProperty("strIngredient9")] string? StrIngredient9;
        [JsonProperty("strIngredient10")] string? StrIngredient10;
        [JsonProperty("strIngredient11")] string? StrIngredient11;
        [JsonProperty("strIngredient12")] string? StrIngredient12;
        [JsonProperty("strIngredient13")] string? StrIngredient13;
        [JsonProperty("strIngredient14")] string? StrIngredient14;
        [JsonProperty("strIngredient15")] string? StrIngredient15;
        [JsonProperty("strMeasure1")] string? StrMeasure1;
        [JsonProperty("strMeasure2")] string? StrMeasure2;
        [JsonProperty("strMeasure3")] string? StrMeasure3;
        [JsonProperty("strMeasure4")] string? StrMeasure4;
        [JsonProperty("strMeasure5")] string? StrMeasure5;
        [JsonProperty("strMeasure6")] string? StrMeasure6;
        [JsonProperty("strMeasure7")] string? StrMeasure7;
        [JsonProperty("strMeasure8")] string? StrMeasure8;
        [JsonProperty("strMeasure9")] string? StrMeasure9;
        [JsonProperty("strMeasure10")] string? StrMeasure10;
        [JsonProperty("strMeasure11")] string? StrMeasure11;
        [JsonProperty("strMeasure12")] string? StrMeasure12;
        [JsonProperty("strMeasure13")] string? StrMeasure13;
        [JsonProperty("strMeasure14")] string? StrMeasure14;
        [JsonProperty("strMeasure15")] string? StrMeasure15;
        [JsonProperty("strImageSource")] string? StrImageSource;
        [JsonProperty("strImageAttribution")] Uri? StrImageAttribution;
        [JsonProperty("strCreativeCommonsConfirmed")] string? StrCreativeCommonsConfirmed;
        [JsonProperty("dateModified")] DateTime DateModified;
    }

    public class DrinkInfos
    {
        [JsonProperty("drinks")] 
        public List<DrinkInfo> DrinkInfoList;
    }
}
