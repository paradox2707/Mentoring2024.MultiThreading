using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Recipes;

public class BasicEnglandRecipe : IRecipe
{
    public void Cook(ICooker cooker)
    {
        cooker.FryRice(100, Level.Low);
        cooker.FryChicken(100, Level.Low);
    }
}
