namespace Task2_1.Interfaces;

public interface IPlayer : IObserver
{
    string PlayerId { get; }
    int SoldShares { get; }
    int BoughtShares { get; }
}

