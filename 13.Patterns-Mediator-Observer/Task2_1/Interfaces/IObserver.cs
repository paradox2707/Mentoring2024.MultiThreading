namespace Task2_1.Interfaces
{
    public interface IObserver
    {
        void NotifySold(string playerId, int numberOfShares);
        void NotifyBought(string playerId, int numberOfShares);
    }
}
