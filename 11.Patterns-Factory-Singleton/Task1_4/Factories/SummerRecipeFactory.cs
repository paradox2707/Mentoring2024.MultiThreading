using Task1_4.Enums;
using Task1_4.Interfaces;
using Task1_4.Recipes;

namespace Task1_4.Factories;

public class SummerRecipeFactory : IRecipeFactory
{
    public IRecipe CreateRecipe(Country country)
    {
        return country switch
        {
            Country.India => new SummerIndiaRecipe(),
            Country.Ukraine => new SummerUkraineRecipe(),
            Country.England => new SummerEnglandRecipe(),
            _ => throw new ArgumentException("Invalid country")
        };
    }
}
