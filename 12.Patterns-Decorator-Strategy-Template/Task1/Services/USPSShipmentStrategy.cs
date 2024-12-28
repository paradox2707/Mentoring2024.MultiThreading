using Task1.Consts;
using Task1.Interfaces;
using Task1.Models;

namespace Task1.Services;

public class USPSShipmentStrategy : IShipmentStrategy
{
    public double CalculatePrice(Order order)
    {
        return order.Product == ProductType.Book ? 3.00d * 0.9 : 3.00d;
    }
}
