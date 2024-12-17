using Moq;
using Task2.Importers;
using Task2.Interfaces;
using Task2.Models;

namespace Task2.Tests
{
    public class Delta1FeedImporterTests
    {
        [Fact]
        public void Import_ValidFeed_SavesFeed()
        {
            // Arrange
            var repositoryMock = new Mock<IDatabaseRepository>();
            var validatorMock = new Mock<IFeedValidator<DeltaOneFeed>>();
            var matcherMock = new Mock<IFeedMatcher<DeltaOneFeed>>();

            var feed = new DeltaOneFeed
            {
                StagingId = 1,
                CounterpartyId = 1,
                PrincipalId = 1,
                SourceAccountId = 1,
                CurrentPrice = 12.34m,
                Isin = "US1234567890",
                ValuationDate = DateTime.Today,
                MaturityDate = DateTime.Today.AddDays(1)
            };

            validatorMock.Setup(v => v.Validate(feed)).Returns(new ValidateResult { IsValid = true });
            repositoryMock.Setup(r => r.LoadFeeds<DeltaOneFeed>()).Returns(new List<DeltaOneFeed>());
            matcherMock.Setup(m => m.Match(It.IsAny<DeltaOneFeed>(), It.IsAny<DeltaOneFeed>())).Returns(false);

            var importer = new Delta1FeedImporter(repositoryMock.Object, validatorMock.Object, matcherMock.Object);

            // Act
            importer.Import(new List<DeltaOneFeed> { feed });

            // Assert
            repositoryMock.Verify(r => r.SaveFeed(feed), Times.Once);
        }

        [Fact]
        public void Import_InvalidFeed_SavesErrors()
        {
            // Arrange
            var repositoryMock = new Mock<IDatabaseRepository>();
            var validatorMock = new Mock<IFeedValidator<DeltaOneFeed>>();
            var matcherMock = new Mock<IFeedMatcher<DeltaOneFeed>>();

            var feed = new DeltaOneFeed
            {
                StagingId = 1,
                CounterpartyId = 1,
                PrincipalId = 1,
                SourceAccountId = 1,
                CurrentPrice = 12.34m,
                Isin = "US1234567890",
                ValuationDate = DateTime.Today,
                MaturityDate = DateTime.Today.AddDays(1)
            };

            var errors = new List<string> { "Error1", "Error2" };
            validatorMock.Setup(v => v.Validate(feed)).Returns(new ValidateResult { IsValid = false, Errors = errors });

            var importer = new Delta1FeedImporter(repositoryMock.Object, validatorMock.Object, matcherMock.Object);

            // Act
            importer.Import(new List<DeltaOneFeed> { feed });

            // Assert
            repositoryMock.Verify(r => r.SaveErrors(feed.StagingId, errors), Times.Once);
        }

        [Fact]
        public void Import_DuplicateFeed_DoesNotSaveFeed()
        {
            // Arrange
            var repositoryMock = new Mock<IDatabaseRepository>();
            var validatorMock = new Mock<IFeedValidator<DeltaOneFeed>>();
            var matcherMock = new Mock<IFeedMatcher<DeltaOneFeed>>();

            var feed = new DeltaOneFeed
            {
                StagingId = 1,
                CounterpartyId = 1,
                PrincipalId = 1,
                SourceAccountId = 1,
                CurrentPrice = 12.34m,
                Isin = "US1234567890",
                ValuationDate = DateTime.Today,
                MaturityDate = DateTime.Today.AddDays(1)
            };

            validatorMock.Setup(v => v.Validate(feed)).Returns(new ValidateResult { IsValid = true });
            repositoryMock.Setup(r => r.LoadFeeds<DeltaOneFeed>()).Returns(new List<DeltaOneFeed> { feed });
            matcherMock.Setup(m => m.Match(feed, feed)).Returns(true);

            var importer = new Delta1FeedImporter(repositoryMock.Object, validatorMock.Object, matcherMock.Object);

            // Act
            importer.Import(new List<DeltaOneFeed> { feed });

            // Assert
            repositoryMock.Verify(r => r.SaveFeed(feed), Times.Never);
        }
    }
}
