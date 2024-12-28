using Task1.Interfaces;
using Task1.Models;

namespace Task1.Services;

public class FedExShipmentStrategy : IShipmentStrategy
{
    public double CalculatePrice(Order order)
    {
        return order.Weight > 300 ? 5.00d * 1.1 : 5.00d;
    }
}
