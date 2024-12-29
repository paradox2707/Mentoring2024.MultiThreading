using Task1.Interfaces;

namespace Task1.Services;

public class StockExchangeMediator : IStockExchangeMediator
{
    private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> sellOffers = new();
    private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> buyOffers = new();
    private readonly Dictionary<string, IPlayer> players = new();

    public void RegisterPlayer(IPlayer player)
    {
        players[player.PlayerId] = player;
    }

    public bool SellOffer(string playerId, string stockName, int numberOfShares)
    {
        if (buyOffers.ContainsKey(stockName) && buyOffers[stockName].Count > 0)
        {
            var offer = buyOffers[stockName][0];
            if (offer.playerId != playerId)
            {
                buyOffers[stockName].RemoveAt(0);
                players[playerId].NotifySold(numberOfShares);
                players[offer.playerId].NotifyBought(numberOfShares);
                return true;
            }
        }

        if (!sellOffers.ContainsKey(stockName))
        {
            sellOffers[stockName] = new List<(string playerId, int numberOfShares)>();
        }

        sellOffers[stockName].Add((playerId, numberOfShares));
        return false;
    }

    public bool BuyOffer(string playerId, string stockName, int numberOfShares)
    {
        if (sellOffers.ContainsKey(stockName) && sellOffers[stockName].Count > 0)
        {
            var offer = sellOffers[stockName][0];
            if (offer.playerId != playerId)
            {
                sellOffers[stockName].RemoveAt(0);
                players[playerId].NotifyBought(numberOfShares);
                players[offer.playerId].NotifySold(numberOfShares);
                return true;
            }
        }

        if (!buyOffers.ContainsKey(stockName))
        {
            buyOffers[stockName] = new List<(string playerId, int numberOfShares)>();
        }

        buyOffers[stockName].Add((playerId, numberOfShares));
        return false;
    }
}

