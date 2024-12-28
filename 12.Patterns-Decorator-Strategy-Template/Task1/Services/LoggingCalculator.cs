using Task1.Interfaces;

public class LoggingCalculator : ICalculator
{
    private readonly ICalculator _innerCalculator;
    private readonly ILogger _logger;

    public LoggingCalculator(ICalculator innerCalculator, ILogger logger)
    {
        _innerCalculator = innerCalculator;
        _logger = logger;
    }

    public decimal CalculatePayment(string touristName)
    {
        _logger.Log("Start");
        var payment = _innerCalculator.CalculatePayment(touristName);
        _logger.Log("End");
        return payment;
    }
}
