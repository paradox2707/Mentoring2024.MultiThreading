using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Recipes;

public class SummerIndiaRecipe : IRecipe
{
    public void Cook(ICooker cooker)
    {
        cooker.FryRice(100, Level.Low);
        cooker.FryChicken(100, Level.Low);
        cooker.PepperRice(Level.Medium);
        cooker.PepperChicken(Level.Medium);
    }
}