using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Recipes;

public class SummerEnglandRecipe : IRecipe
{
    public void Cook(ICooker cooker)
    {
        cooker.FryRice(50, Level.Low);
        cooker.FryChicken(50, Level.Low);
    }
}
