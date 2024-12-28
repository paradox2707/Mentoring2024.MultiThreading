using Task1.Interfaces;

namespace Task1.Services;

public class CalculatorFactory : ICalculatorFactory
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;
    private readonly ILogger _logger;

    public CalculatorFactory(ICurrencyService currencyService, ITripRepository tripRepository, ILogger logger)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
        _logger = logger;
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

    public ICalculator CreateLoggingCalculator()
    {
        var basicCalculator = new InsurancePaymentCalculator(_currencyService, _tripRepository);
        return new LoggingCalculator(basicCalculator, _logger);
    }

    public ICalculator CreateRoundingCalculator()
    {
        var basicCalculator = new InsurancePaymentCalculator(_currencyService, _tripRepository);
        return new RoundingCalculator(basicCalculator);
    }
}
