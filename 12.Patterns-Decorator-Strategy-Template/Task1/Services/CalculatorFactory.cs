using Task1.Interfaces;

namespace Task1.Services;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;

    public CalculatorFactory(ICurrencyService currencyService, ITripRepository tripRepository)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
    }

    public ICalculator CreateCalculator()
    {
        return new InsurancePaymentCalculator(_currencyService, _tripRepository);
    }

    public ICalculator CreateCachedCalculator()
    {
        var basicCalculator = new InsurancePaymentCalculator(_currencyService, _tripRepository);
        return new CachedInsurancePaymentCalculator(basicCalculator);
    }
}
