namespace Task1.Interfaces;

public interface IPlayer
{
    string PlayerId { get; }
    int SoldShares { get; }
    int BoughtShares { get; }
    void NotifySold(int numberOfShares);
    void NotifyBought(int numberOfShares);
}

