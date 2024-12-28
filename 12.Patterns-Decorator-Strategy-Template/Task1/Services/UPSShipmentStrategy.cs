using Task1.Interfaces;
using Task1.Models;

namespace Task1.Services;

public class UPSShipmentStrategy : IShipmentStrategy
{
    public double CalculatePrice(Order order)
    {
        return order.Weight > 400 ? 4.25d * 1.1 : 4.25d;
    }
}
