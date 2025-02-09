using Task1_4.Enums;
using Task1_4.Interfaces;
using Task1_4.Recipes;

namespace Task1_4.Factories;

public class BasicRecipeFactory : IRecipeFactory
{
    public IRecipe CreateRecipe(Country country)
    {
        return country switch
        {
            Country.India => new BasicIndiaRecipe(),
            Country.Ukraine => new BasicUkraineRecipe(),
            Country.England => new BasicEnglandRecipe(),
            _ => throw new ArgumentException("Invalid country")
        };
    }
}
