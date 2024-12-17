using Task2.Models;
using Task2.Validators;

namespace Task2.Tests
{
    public class EmFeedValidatorTests
    {
        [Fact]
        public void Validate_ValidEmFeed_ReturnsValidResult()
        {
            // Arrange
            var feed = new EmFeed
            {
                StagingId = 1,
                CounterpartyId = 1,
                PrincipalId = 1,
                SourceAccountId = 1,
                CurrentPrice = 12.34m,
                Sedol = 50m,
                AssetValue = 25m
            };
            var validator = new EmFeedValidator();

            // Act
            var result = validator.Validate(feed);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Validate_InvalidEmFeed_ReturnsInvalidResult()
        {
            // Arrange
            var feed = new EmFeed
            {
                StagingId = 0,
                CounterpartyId = -1,
                PrincipalId = 0,
                SourceAccountId = 0,
                CurrentPrice = 45.6788m,
                Sedol = 100m,
                AssetValue = 101m
            };
            var validator = new EmFeedValidator();

            // Act
            var result = validator.Validate(feed);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(ErrorCode.InvalidIdentifier, result.Errors);
            Assert.Contains(ErrorCode.InvalidPrice, result.Errors);
            Assert.Contains(ErrorCode.InvalidSedol, result.Errors);
            Assert.Contains(ErrorCode.InvalidAssetValue, result.Errors);
        }
    }
}
