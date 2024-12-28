using Task1.Interfaces;

public class RoundingCalculator : ICalculator
{
    private readonly ICalculator _innerCalculator;

    public RoundingCalculator(ICalculator innerCalculator)
    {
        _innerCalculator = innerCalculator;
    }

    public decimal CalculatePayment(string touristName)
    {
        var payment = _innerCalculator.CalculatePayment(touristName);
        return Math.Round(payment, MidpointRounding.AwayFromZero);
    }
}
