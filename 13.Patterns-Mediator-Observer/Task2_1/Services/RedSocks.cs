using Task2_1.Interfaces;

namespace Task2_1.Services;

public class RedSocks : IPlayer
{
    private readonly IStockExchangeMediator _mediator;
    public string PlayerId { get; }
    public int SoldShares { get; private set; }
    public int BoughtShares { get; private set; }

    public RedSocks(IStockExchangeMediator mediator)
    {
        _mediator = mediator;
        PlayerId = Guid.NewGuid().ToString();
        _mediator.RegisterPlayer(this);
    }

    public bool SellOffer(string stockName, int numberOfShares)
    {
        return _mediator.SellOffer(PlayerId, stockName, numberOfShares);
    }

    public bool BuyOffer(string stockName, int numberOfShares)
    {
        return _mediator.BuyOffer(PlayerId, stockName, numberOfShares);
    }

    public void NotifySold(string playerId, int numberOfShares)
    {
        if (playerId == PlayerId)
        {
            SoldShares += numberOfShares;
        }
    }

    public void NotifyBought(string playerId, int numberOfShares)
    {
        if (playerId == PlayerId)
        {
            BoughtShares += numberOfShares;
        }
    }
}

