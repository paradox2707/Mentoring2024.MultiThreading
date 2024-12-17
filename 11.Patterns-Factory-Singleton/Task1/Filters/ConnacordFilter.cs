using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

public class ConnacordFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(t => t.Type == "Future" && t.Amount > 10 && t.Amount < 40);
    }
}