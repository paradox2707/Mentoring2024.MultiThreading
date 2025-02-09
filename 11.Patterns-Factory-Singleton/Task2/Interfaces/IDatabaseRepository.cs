using System.Collections.Generic;

namespace Task2.Interfaces;

public interface IDatabaseRepository
{
    List<T> LoadFeeds<T>();
    void SaveFeed<T>(T feed);
    void SaveErrors(int feedStagingId, List<string> errors);
}
