namespace Task1
{
    public class TradeFilter
    {
        private readonly IFilterFactory _filterFactory;

        public TradeFilter(IFilterFactory filterFactory)
        {
            _filterFactory = filterFactory;
        }

        public IEnumerable<Trade> FilterForBank(IEnumerable<Trade> trades, Bank bank, Country country)
        {
            var filter = _filterFactory.CreateFilter(bank, country);
            return filter.Match(trades);
        }
    }
}