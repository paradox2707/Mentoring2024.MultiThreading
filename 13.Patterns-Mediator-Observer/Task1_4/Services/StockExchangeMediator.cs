using System;
using System.Collections.Generic;
using Task1_4.Interfaces;

namespace Task1_4.Services
{
    public class StockExchangeMediator : IStockExchangeMediator
    {
        private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> sellOffers = new();
        private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> buyOffers = new();
        private readonly Dictionary<string, IPlayer> players = new();

        public event Action<string, int> OnSharesSold;
        public event Action<string, int> OnSharesBought;

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
                    OnSharesSold?.Invoke(playerId, numberOfShares);
                    OnSharesBought?.Invoke(offer.playerId, numberOfShares);
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
                    OnSharesBought?.Invoke(playerId, numberOfShares);
                    OnSharesSold?.Invoke(offer.playerId, numberOfShares);
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
}

