namespace Task2_1.Interfaces
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void UnregisterObserver(IObserver observer);
        void NotifyObserversSold(string playerId, int numberOfShares);
        void NotifyObserversBought(string playerId, int numberOfShares);
    }
}
