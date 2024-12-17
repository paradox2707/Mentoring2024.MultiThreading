namespace Task1_4
{
    public class EnglandMasalaFactory : IMasalaFactory
    {
        public void CookMasala(ICooker cooker)
        {
            cooker.FryRice(100, Level.Low);
            cooker.FryChicken(100, Level.Low);
            // No salt or pepper for England
        }
    }
}