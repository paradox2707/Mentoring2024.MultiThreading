namespace Task1_4
{
    public class IndiaMasalaFactory : IMasalaFactory
    {
        public void CookMasala(ICooker cooker)
        {
            cooker.FryRice(200, Level.Strong);
            cooker.FryChicken(100, Level.Strong);
            cooker.SaltRice(Level.Strong);
            cooker.SaltChicken(Level.Strong);
            cooker.PepperRice(Level.Strong);
            cooker.PepperChicken(Level.Strong);
        }
    }
}