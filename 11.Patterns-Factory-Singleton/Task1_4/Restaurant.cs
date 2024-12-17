namespace Task1_4
{
    public class Restaurant
    {
        public void CookMasala(ICooker cooker, Country country)
        {
            IMasalaFactory factory = country switch
            {
                Country.India => new IndiaMasalaFactory(),
                Country.Ukraine => new UkraineMasalaFactory(),
                Country.England => new EnglandMasalaFactory(),
                _ => throw new NotImplementedException("Unsupported country")
            };

            factory.CookMasala(cooker);
        }
    }
}