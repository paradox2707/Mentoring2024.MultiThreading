using Task1.Consts;
using Task1.Interfaces;
using Task1.Models;

namespace Task1.Services;

public class ShipmentCalculator
{
    private readonly Dictionary<ShipmentOptions, IShipmentStrategy> _strategies;

    public ShipmentCalculator()
    {
        _strategies = new Dictionary<ShipmentOptions, IShipmentStrategy>
        {
            { ShipmentOptions.FedEx, new FedExShipmentStrategy() },
            { ShipmentOptions.UPS, new UPSShipmentStrategy() },
            { ShipmentOptions.USPS, new USPSShipmentStrategy() }
        };
    }

    public double CalculatePrice(Order order)
    {
        if (_strategies.TryGetValue(order.ShipmentOptions, out var strategy))
        {
            return strategy.CalculatePrice(order);
        }
        throw new NotImplementedException();
    }
}
