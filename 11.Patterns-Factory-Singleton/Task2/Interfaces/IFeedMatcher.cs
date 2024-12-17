using Task2.Models;

namespace Task2.Interfaces;

public interface IFeedMatcher<T> where T : TradeFeed
{
    bool Match(T current, T other);
}
