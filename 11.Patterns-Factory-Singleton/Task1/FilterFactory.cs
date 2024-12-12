namespace Task1
{
    namespace Task1
    {
        public class FilterFactory : IFilterFactory
        {
            public IFilter CreateFilter(Bank bank, Country country)
            {
                return bank switch
                {
                    Bank.Bofa => new BofaFilter(),
                    Bank.Connacord => new ConnacordFilter(),
                    Bank.Barclays => country switch
                    {
                        Country.USA => new BarclaysFilter(),
                        Country.England => new BarclaysEnglandFilter(),
                        _ => throw new NotImplementedException("Unsupported country")
                    },
                    Bank.Deutsche => new DeutscheFilter(),
                    _ => throw new NotImplementedException("Unsupported bank")
                };
            }
        }
    }
}