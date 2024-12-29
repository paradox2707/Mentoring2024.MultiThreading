namespace Task1_4.Interfaces;

public interface IPlayer
{
    string PlayerId { get; }
    int SoldShares { get; }
    int BoughtShares { get; }
    void NotifySold(string playerId, int numberOfShares);
    void NotifyBought(string playerId, int numberOfShares);
}

