namespace Task2_1.Interfaces
{
    public interface IStockExchangeMediator : ISubject
    {
        bool SellOffer(string playerId, string stockName, int numberOfShares);
        bool BuyOffer(string playerId, string stockName, int numberOfShares);
        void RegisterPlayer(IPlayer player);
    }
}
