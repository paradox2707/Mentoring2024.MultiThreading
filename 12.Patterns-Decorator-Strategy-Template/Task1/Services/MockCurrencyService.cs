using Task1.Interfaces;

namespace Task1.Services;

public class MockCurrencyService : ICurrencyService
{
    public decimal LoadCurrencyRate()
    {
        return 1.2m; // Example rate
    }
}
