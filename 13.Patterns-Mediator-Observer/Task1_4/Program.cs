using Task1_4.Interfaces;
using Task1_4.Services;

namespace Task1_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mediator = new StockExchangeMediator();

            var redSocks = new RedSocks(mediator);
            var blossomers = new Blossomers(mediator);
            var rossStones = new RossStones(mediator);

            // Simulation
            Console.WriteLine("Initial State:");
            PrintPlayerStatus(redSocks, blossomers, rossStones);

            // Blossomers tries to buy 2 shares of "RTC"
            Console.WriteLine("\nBlossomers tries to buy 2 shares of 'RTC'");
            blossomers.BuyOffer("RTC", 2);
            PrintPlayerStatus(redSocks, blossomers, rossStones);

            // RedSocks sells 2 shares of "RTC"
            Console.WriteLine("\nRedSocks sells 2 shares of 'RTC'");
            redSocks.SellOffer("RTC", 2);
            PrintPlayerStatus(redSocks, blossomers, rossStones);

            // RossStones tries to buy 3 shares of "ABC"
            Console.WriteLine("\nRossStones tries to buy 3 shares of 'ABC'");
            rossStones.BuyOffer("ABC", 3);
            PrintPlayerStatus(redSocks, blossomers, rossStones);

            // Blossomers sells 3 shares of "ABC"
            Console.WriteLine("\nBlossomers sells 3 shares of 'ABC'");
            blossomers.SellOffer("ABC", 3);
            PrintPlayerStatus(redSocks, blossomers, rossStones);
        }

        static void PrintPlayerStatus(params IPlayer[] players)
        {
            foreach (var player in players)
            {
                Console.WriteLine($"{player.GetType().Name} - SoldShares: {player.SoldShares}, BoughtShares: {player.BoughtShares}");
            }
        }
    }
}
