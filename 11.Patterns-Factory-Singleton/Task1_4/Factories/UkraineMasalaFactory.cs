using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Factories;

public class UkraineMasalaFactory : IMasalaFactory
{
    public void CookMasala(ICooker cooker)
    {
        cooker.FryRice(500, Level.Strong);
        cooker.FryChicken(300, Level.Medium);
        cooker.SaltRice(Level.Strong);
        cooker.SaltChicken(Level.Medium);
        cooker.PepperRice(Level.Low);
        cooker.PepperChicken(Level.Low);
    }
}