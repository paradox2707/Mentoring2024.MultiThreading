using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

public class BarclaysEnglandFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(trade => trade.Type == "Future" && trade.Amount > 100);
    }
}
