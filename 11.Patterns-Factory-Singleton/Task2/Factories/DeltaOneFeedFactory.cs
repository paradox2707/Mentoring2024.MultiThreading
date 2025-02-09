using Task2.Importers;
using Task2.Interfaces;
using Task2.Matchers;
using Task2.Models;
using Task2.Validators;

namespace Task2.Factories;

public class DeltaOneFeedFactory : IFeedFactory<DeltaOneFeed>
{
    public IFeedValidator<DeltaOneFeed> CreateValidator()
    {
        return new DeltaOneFeedValidator();
    }

    public IFeedMatcher<DeltaOneFeed> CreateMatcher()
    {
        return new DeltaOneFeedMatcher();
    }

    public BaseFeedImporter<DeltaOneFeed> CreateImporter(IDatabaseRepository repository)
    {
        return new Delta1FeedImporter(repository, CreateValidator(), CreateMatcher());
    }
}
