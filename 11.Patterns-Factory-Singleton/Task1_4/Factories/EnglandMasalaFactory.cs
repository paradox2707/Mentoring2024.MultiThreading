using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Factories;

public class EnglandMasalaFactory : IMasalaFactory
{
    public void CookMasala(ICooker cooker)
    {
        cooker.FryRice(100, Level.Low);
        cooker.FryChicken(100, Level.Low);
        // No salt or pepper for England
    }
}