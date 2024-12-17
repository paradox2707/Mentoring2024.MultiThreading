using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Recipes;

public class BasicIndiaRecipe : IRecipe
{
    public void Cook(ICooker cooker)
    {
        cooker.FryRice(200, Level.Strong);
        cooker.FryChicken(100, Level.Strong);
        cooker.SaltRice(Level.Strong);
        cooker.SaltChicken(Level.Strong);
        cooker.PepperRice(Level.Strong);
        cooker.PepperChicken(Level.Strong);
    }
}