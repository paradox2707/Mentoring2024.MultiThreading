using System.Collections.Generic;
using System.Linq;
using Task2.Interfaces;
using Task2.Models;

namespace Task2.Importers;

public abstract class BaseFeedImporter<T> where T : TradeFeed
{
    private readonly IDatabaseRepository _repository;
    private readonly IFeedValidator<T> _validator;
    private readonly IFeedMatcher<T> _matcher;

    protected BaseFeedImporter(IDatabaseRepository repository, IFeedValidator<T> validator, IFeedMatcher<T> matcher)
    {
        _repository = repository;
        _validator = validator;
        _matcher = matcher;
    }

    public void Import(IEnumerable<T> feeds)
    {
        var existingFeeds = _repository.LoadFeeds<T>();

        foreach (var feed in feeds)
        {
            var validationResult = _validator.Validate(feed);
            if (!validationResult.IsValid)
            {
                _repository.SaveErrors(feed.StagingId, validationResult.Errors);
                continue;
            }

            if (!existingFeeds.Any(existingFeed => _matcher.Match(feed, existingFeed)))
            {
                _repository.SaveFeed(feed);
            }
        }
    }
}