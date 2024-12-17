using Moq;
using Task2.Factories;
using Task2.Importers;
using Task2.Interfaces;
using Task2.Matchers;
using Task2.Validators;
using Xunit;

namespace Task2.Tests;

public class EmFeedFactoryTests
{
    [Fact]
    public void CreateValidator_ReturnsEmFeedValidator()
    {
        // Arrange
        var factory = new EmFeedFactory();

        // Act
        var validator = factory.CreateValidator();

        // Assert
        Assert.IsType<EmFeedValidator>(validator);
    }

    [Fact]
    public void CreateMatcher_ReturnsEmFeedMatcher()
    {
        // Arrange
        var factory = new EmFeedFactory();

        // Act
        var matcher = factory.CreateMatcher();

        // Assert
        Assert.IsType<EmFeedMatcher>(matcher);
    }

    [Fact]
    public void CreateImporter_ReturnsEmFeedImporter()
    {
        // Arrange
        var factory = new EmFeedFactory();
        var repositoryMock = new Mock<IDatabaseRepository>();

        // Act
        var importer = factory.CreateImporter(repositoryMock.Object);

        // Assert
        Assert.IsType<EmFeedImporter>(importer);
    }
}
