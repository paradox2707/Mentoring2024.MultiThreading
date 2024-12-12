namespace Task1
{
    public class BarclaysEnglandFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(trade => trade.Type == "Future" && trade.Amount > 100);
        }
    }
}
