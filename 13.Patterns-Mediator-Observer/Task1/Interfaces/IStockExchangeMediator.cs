namespace Task1.Interfaces;

public interface IStockExchangeMediator
{
    bool SellOffer(string playerId, string stockName, int numberOfShares);
    bool BuyOffer(string playerId, string stockName, int numberOfShares);
    void RegisterPlayer(IPlayer player);
}