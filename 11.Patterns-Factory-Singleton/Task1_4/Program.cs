namespace Task1_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var restaurant = new Restaurant();
            var cooker = new ExampleCooker(new Outputer());

            restaurant.CookMasala(cooker, Country.India);
            restaurant.CookMasala(cooker, Country.Ukraine);
            restaurant.CookMasala(cooker, Country.England);
        }
    }
}
