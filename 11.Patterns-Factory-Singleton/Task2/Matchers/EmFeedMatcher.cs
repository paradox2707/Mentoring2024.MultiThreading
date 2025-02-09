using Task2.Interfaces;
using Task2.Models;

namespace Task2.Matchers;

public class EmFeedMatcher : IFeedMatcher<EmFeed>
{
    public bool Match(EmFeed current, EmFeed other)
    {
        if (current.SourceAccountId != 0 && other.SourceAccountId != 0)
        {
            return current.SourceAccountId == other.SourceAccountId;
        }
        return current.StagingId == other.StagingId;
    }
}