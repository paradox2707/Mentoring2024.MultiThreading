using Moq;
using Task2.Factories;
using Task2.Importers;
using Task2.Interfaces;
using Task2.Matchers;
using Task2.Validators;
using Xunit;

namespace Task2.Tests
{
    public class DeltaOneFeedFactoryTests
    {
        [Fact]
        public void CreateValidator_ReturnsDeltaOneFeedValidator()
        {
            // Arrange
            var factory = new DeltaOneFeedFactory();

            // Act
            var validator = factory.CreateValidator();

            // Assert
            Assert.IsType<DeltaOneFeedValidator>(validator);
        }

        [Fact]
        public void CreateMatcher_ReturnsDeltaOneFeedMatcher()
        {
            // Arrange
            var factory = new DeltaOneFeedFactory();

            // Act
            var matcher = factory.CreateMatcher();

            // Assert
            Assert.IsType<DeltaOneFeedMatcher>(matcher);
        }

        [Fact]
        public void CreateImporter_ReturnsDelta1FeedImporter()
        {
            // Arrange
            var factory = new DeltaOneFeedFactory();
            var repositoryMock = new Mock<IDatabaseRepository>();

            // Act
            var importer = factory.CreateImporter(repositoryMock.Object);

            // Assert
            Assert.IsType<Delta1FeedImporter>(importer);
        }
    }
}
