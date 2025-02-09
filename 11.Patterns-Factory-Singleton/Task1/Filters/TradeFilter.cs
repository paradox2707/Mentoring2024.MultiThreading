using Task1.Enums;
using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

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