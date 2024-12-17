using Task1_4.Enums;
using Task1_4.Factories;
using Task1_4.Interfaces;

namespace Task1_4;

public class Restaurant
{
    public void CookMasala(ICooker cooker, Country country, DateTime currentTime)
    {
        IRecipeFactory factory = currentTime.Month switch
        {
            6 or 7 or 8 => new SummerRecipeFactory(),
            _ => new BasicRecipeFactory()
        };

        var recipe = factory.CreateRecipe(country);
        recipe.Cook(cooker);
    }
}