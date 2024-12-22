using Task1.Interfaces;

namespace Task1.Services;

public class CachedInsurancePaymentCalculator : ICalculator
{
    private readonly ICalculator _innerCalculator;
    private readonly Dictionary<string, decimal> _cache;

    public CachedInsurancePaymentCalculator(ICalculator innerCalculator)
    {
        _innerCalculator = innerCalculator;
        _cache = new Dictionary<string, decimal>();
    }

    public decimal CalculatePayment(string touristName)
    {
        if (_cache.TryGetValue(touristName, out var cachedPayment))
        {
            return cachedPayment;
        }

        var payment = _innerCalculator.CalculatePayment(touristName);
        _cache[touristName] = payment;

        return payment;
    }
}
