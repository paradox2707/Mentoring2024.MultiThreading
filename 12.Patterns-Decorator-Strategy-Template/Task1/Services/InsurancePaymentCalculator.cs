using Task1.Interfaces;

namespace Task1.Services;

public class InsurancePaymentCalculator : ICalculator
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;

    public InsurancePaymentCalculator(ICurrencyService currencyService, ITripRepository tripRepository)
    {
        _currencyService = currencyService;
        _tripRepository = tripRepository;
    }

    public decimal CalculatePayment(string touristName)
    {
        // Simulate slow operations
        var currencyRate = _currencyService.LoadCurrencyRate();
        var tripDetails = _tripRepository.LoadTrip(touristName);

        var tripCost = tripDetails.FlyCost + tripDetails.AccomodationCost + tripDetails.ExcursionCost;
        return tripCost * currencyRate;
    }
}
