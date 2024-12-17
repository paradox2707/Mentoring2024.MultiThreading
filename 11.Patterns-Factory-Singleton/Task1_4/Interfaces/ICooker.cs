using Task1_4.Enums;

namespace Task1_4.Interfaces;

public interface ICooker
{
    void FryRice(int amount, Level level);
    void FryChicken(int amount, Level level);
    void SaltRice(Level level);
    void SaltChicken(Level level);
    void PepperRice(Level level);
    void PepperChicken(Level level);
}