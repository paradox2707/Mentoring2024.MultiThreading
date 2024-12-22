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
        ICalculatorFactory calculatorFactory = new CalculatorFactory(currencyService, tripRepository);

        ICalculator calculator = calculatorFactory.CreateCachedCalculator();

        string touristName = "John Doe";
        decimal payment = calculator.CalculatePayment(touristName);

        Console.WriteLine($"Insurance payment for {touristName}: {payment}");
    }
}
