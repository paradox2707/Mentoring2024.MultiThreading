using Task1.Interfaces;
using Task1.Repos;
using Task1.Services;

namespace Task1;

internal class Program
{
    static void Main(string[] args)
    {
        // Example usage
        ICurrencyService currencyService = new MockCurrencyService();
        ITripRepository tripRepository = new MockTripRepository();
        ILogger logger = new ConsoleLogger();
        ICalculatorFactory calculatorFactory = new CalculatorFactory(currencyService, tripRepository, logger);

        // Using the LoggingCalculator
        ICalculator loggingCalculator = calculatorFactory.CreateLoggingCalculator();
        string touristName = "John Doe";
        decimal loggingPayment = loggingCalculator.CalculatePayment(touristName);
        Console.WriteLine($"Insurance payment for {touristName} with logging: {loggingPayment}");

        // Using the RoundingCalculator
        ICalculator roundingCalculator = calculatorFactory.CreateRoundingCalculator();
        decimal roundingPayment = roundingCalculator.CalculatePayment(touristName);
        Console.WriteLine($"Insurance payment for {touristName} with rounding: {roundingPayment}");
    }
}
