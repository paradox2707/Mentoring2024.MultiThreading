using Task1.Interfaces;

namespace Task1.Services;

public class RossStones : IPlayer
{
    private readonly IStockExchangeMediator _mediator;
    public string PlayerId { get; }
    public int SoldShares { get; private set; }
    public int BoughtShares { get; private set; }

    public RossStones(IStockExchangeMediator mediator)
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

    public void NotifySold(int numberOfShares)
    {
        SoldShares += numberOfShares;
    }

    public void NotifyBought(int numberOfShares)
    {
        BoughtShares += numberOfShares;
    }
}

