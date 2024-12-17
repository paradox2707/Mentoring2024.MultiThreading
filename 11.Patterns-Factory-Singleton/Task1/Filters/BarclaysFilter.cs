using Task1.Interfaces;
using Task1.Models;

namespace Task1.Filters;

public class BarclaysFilter : IFilter
{
    public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
    {
        return trades.Where(t => t.Type == "Option" && t.SubType == "NyOption" && t.Amount > 50);
    }
}
