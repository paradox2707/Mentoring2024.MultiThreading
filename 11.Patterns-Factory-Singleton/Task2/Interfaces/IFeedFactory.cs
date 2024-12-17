using Task2.Importers;
using Task2.Models;

namespace Task2.Interfaces;

public interface IFeedFactory<T> where T : TradeFeed
{
    IFeedValidator<T> CreateValidator();
    IFeedMatcher<T> CreateMatcher();
    BaseFeedImporter<T> CreateImporter(IDatabaseRepository repository);
}
