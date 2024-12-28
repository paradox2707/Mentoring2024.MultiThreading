using System;

namespace Task1
{
    public class RedSocks
    {
        private readonly IStockExchangeMediator _mediator;
        private readonly string _playerId;

        public RedSocks(IStockExchangeMediator mediator)
        {
            _mediator = mediator;
            _playerId = Guid.NewGuid().ToString();
        }

        public bool SellOffer(string stockName, int numberOfShares)
        {
            return _mediator.SellOffer(_playerId, stockName, numberOfShares);
        }

        public bool BuyOffer(string stockName, int numberOfShares)
        {
            return _mediator.BuyOffer(_playerId, stockName, numberOfShares);
        }
    }
}
