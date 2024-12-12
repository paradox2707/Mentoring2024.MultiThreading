namespace Task1
{
    public class DeutscheFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(trade => trade.Type == "Option" && trade.SubType == "NewOption" && trade.Amount > 90 && trade.Amount < 120);
        }
    }
}