using Task1.Interfaces;

public class CachedCalculator : ICalculator
{
    private readonly ICalculator _innerCalculator;
    private readonly Dictionary<string, decimal> _cache = new();

    public CachedCalculator(ICalculator innerCalculator)
    {
        _innerCalculator = innerCalculator;
    }

    public decimal CalculatePayment(string touristName)
    {
        if (_cache.ContainsKey(touristName))
        {
            return _cache[touristName];
        }

        var result = _innerCalculator.CalculatePayment(touristName);
        _cache[touristName] = result;
        return result;
    }
}
