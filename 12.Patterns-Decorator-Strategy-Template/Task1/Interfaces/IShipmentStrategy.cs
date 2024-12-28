using Task1.Models;

namespace Task1.Interfaces;

public interface IShipmentStrategy
{
    double CalculatePrice(Order order);
}
