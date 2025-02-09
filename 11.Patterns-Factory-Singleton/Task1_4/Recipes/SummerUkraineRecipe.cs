using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Recipes;

public class SummerUkraineRecipe : IRecipe
{
    public void Cook(ICooker cooker)
    {
        cooker.FryRice(150, Level.Medium);
        cooker.FryChicken(200, Level.Medium);
        cooker.SaltRice(Level.Low);
        cooker.SaltChicken(Level.Low);
    }
}
