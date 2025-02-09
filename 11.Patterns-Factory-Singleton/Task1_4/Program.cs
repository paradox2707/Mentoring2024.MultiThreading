using Task1_4.Enums;

namespace Task1_4;

internal class Program
{
    static void Main(string[] args)
    {
        var restaurant = new Restaurant();
        var cooker = new ExampleCooker(new Outputer());
        var currentDate = DateTime.Now;

        restaurant.CookMasala(cooker, Country.India, currentDate);
        restaurant.CookMasala(cooker, Country.Ukraine, currentDate);
        restaurant.CookMasala(cooker, Country.England, currentDate);
    }
}
