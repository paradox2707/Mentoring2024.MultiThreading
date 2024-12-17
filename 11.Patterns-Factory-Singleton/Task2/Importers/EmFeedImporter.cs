using Task2.Interfaces;
using Task2.Models;

namespace Task2.Importers;

public class EmFeedImporter : BaseFeedImporter<EmFeed>
{
    public EmFeedImporter(IDatabaseRepository repository, IFeedValidator<EmFeed> validator, IFeedMatcher<EmFeed> matcher)
        : base(repository, validator, matcher)
    {
    }
}