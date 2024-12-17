using Task2.Interfaces;
using Task2.Models;

namespace Task2.Matchers;

public class DeltaOneFeedMatcher : IFeedMatcher<DeltaOneFeed>
{
    public bool Match(DeltaOneFeed current, DeltaOneFeed other)
    {
        return current.CounterpartyId == other.CounterpartyId && current.PrincipalId == other.PrincipalId;
    }
}

