using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

public class DeutscheFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(trade => trade.Type == "Option" && trade.SubType == "NewOption" && trade.Amount > 90 && trade.Amount < 120);
    }
}