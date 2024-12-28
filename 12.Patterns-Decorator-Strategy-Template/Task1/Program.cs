using Task1.Consts;
using Task1.Interfaces;
using Task1.Models;
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

        // Using the CachedLoggingPaymentCalculator
        ICalculator cachedLoggingCalculator = calculatorFactory.CreateDynamicCalculator(
            calc => new CachedCalculator(calc),
            calc => new LoggingCalculator(calc, logger)
        );
        string touristName = "John Doe";
        decimal cachedLoggingPayment = cachedLoggingCalculator.CalculatePayment(touristName);
        Console.WriteLine($"Insurance payment for {touristName} with caching and logging: {cachedLoggingPayment}");

        // Using the RoundingCachedPaymentCalculator
        ICalculator roundingCachedCalculator = calculatorFactory.CreateDynamicCalculator(
            calc => new CachedCalculator(calc),
            calc => new RoundingCalculator(calc)
        );
        decimal roundingCachedPayment = roundingCachedCalculator.CalculatePayment(touristName);
        Console.WriteLine($"Insurance payment for {touristName} with rounding and caching: {roundingCachedPayment}");

        // Example usage of ShipmentCalculator
        var order = new Order(ShipmentOptions.FedEx, 350, ProductType.Book);
        var shipmentCalculator = new ShipmentCalculator();
        double shipmentPrice = shipmentCalculator.CalculatePrice(order);
        Console.WriteLine($"Shipment price for order: {shipmentPrice}");
    }
}
