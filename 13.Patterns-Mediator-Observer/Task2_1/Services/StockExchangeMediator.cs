using System;
using System.Collections.Generic;
using Task2_1.Interfaces;

namespace Task2_1.Services
{
    public class StockExchangeMediator : IStockExchangeMediator
    {
        private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> sellOffers = new();
        private readonly Dictionary<string, List<(string playerId, int numberOfShares)>> buyOffers = new();
        private readonly Dictionary<string, IPlayer> players = new();
        private readonly List<IObserver> observers = new();

        public void RegisterPlayer(IPlayer player)
        {
            players[player.PlayerId] = player;
            RegisterObserver(player);
        }

        public bool SellOffer(string playerId, string stockName, int numberOfShares)
        {
            if (buyOffers.ContainsKey(stockName) && buyOffers[stockName].Count > 0)
            {
                var offer = buyOffers[stockName][0];
                if (offer.playerId != playerId)
                {
                    buyOffers[stockName].RemoveAt(0);
                    NotifyObserversSold(playerId, numberOfShares);
                    NotifyObserversBought(offer.playerId, numberOfShares);
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
                    NotifyObserversBought(playerId, numberOfShares);
                    NotifyObserversSold(offer.playerId, numberOfShares);
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

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObserversSold(string playerId, int numberOfShares)
        {
            foreach (var observer in observers)
            {
                observer.NotifySold(playerId, numberOfShares);
            }
        }

        public void NotifyObserversBought(string playerId, int numberOfShares)
        {
            foreach (var observer in observers)
            {
                observer.NotifyBought(playerId, numberOfShares);
            }
        }
    }
}
