namespace Task1_4.Interfaces;

public interface IStockExchangeMediator
{
    bool SellOffer(string playerId, string stockName, int numberOfShares);
    bool BuyOffer(string playerId, string stockName, int numberOfShares);
    void RegisterPlayer(IPlayer player);

    event Action<string, int> OnSharesSold;
    event Action<string, int> OnSharesBought;
}
