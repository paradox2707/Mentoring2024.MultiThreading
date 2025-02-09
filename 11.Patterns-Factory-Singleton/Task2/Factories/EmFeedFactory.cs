using Task2.Importers;
using Task2.Interfaces;
using Task2.Matchers;
using Task2.Models;
using Task2.Validators;

namespace Task2.Factories;

public class EmFeedFactory : IFeedFactory<EmFeed>
{
    public IFeedValidator<EmFeed> CreateValidator()
    {
        return new EmFeedValidator();
    }

    public IFeedMatcher<EmFeed> CreateMatcher()
    {
        return new EmFeedMatcher();
    }

    public BaseFeedImporter<EmFeed> CreateImporter(IDatabaseRepository repository)
    {
        return new EmFeedImporter(repository, CreateValidator(), CreateMatcher());
    }
}
