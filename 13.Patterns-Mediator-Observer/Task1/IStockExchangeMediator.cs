using System;
using System.Collections.Generic;

namespace Task1;

public interface IStockExchangeMediator
{
    bool SellOffer(string playerId, string stockName, int numberOfShares);
    bool BuyOffer(string playerId, string stockName, int numberOfShares);
}
