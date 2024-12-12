namespace Task1
{
    public class BarclaysFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(t => t.Type == "Option" && t.SubType == "NyOption" && t.Amount > 50);
        }
    }
}
