using Task1_4.Enums;

namespace Task1_4.Interfaces;

public interface IRecipeFactory
{
    IRecipe CreateRecipe(Country country);
}