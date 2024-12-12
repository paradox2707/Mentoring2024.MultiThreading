namespace Task1
{
    public class ConnacordFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(t => t.Type == "Future" && t.Amount > 10 && t.Amount < 40);
        }
    }
}