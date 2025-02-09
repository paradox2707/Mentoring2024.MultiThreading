using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

public class BofaFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(t => t.Amount > 70);
    }
}
