using Task2.Interfaces;
using Task2.Models;

namespace Task2.Importers;

public class Delta1FeedImporter : BaseFeedImporter<DeltaOneFeed>
{
    public Delta1FeedImporter(IDatabaseRepository repository, IFeedValidator<DeltaOneFeed> validator, IFeedMatcher<DeltaOneFeed> matcher)
        : base(repository, validator, matcher)
    {
    }
}