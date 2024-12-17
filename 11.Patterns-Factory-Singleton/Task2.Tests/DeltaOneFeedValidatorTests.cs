using System;
using Task2;
using Xunit;

namespace Task2.Tests
{
    public class DeltaOneFeedValidatorTests
    {
        [Fact]
        public void Validate_ValidDeltaOneFeed_ReturnsValidResult()
        {
            // Arrange
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
            var validator = new DeltaOneFeedValidator();

            // Act
            var result = validator.Validate(feed);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Validate_InvalidDeltaOneFeed_ReturnsInvalidResult()
        {
            // Arrange
            var feed = new DeltaOneFeed
            {
                StagingId = 0,
                CounterpartyId = -1,
                PrincipalId = 0,
                SourceAccountId = 0,
                CurrentPrice = 45.6788m,
                Isin = "1234567890",
                ValuationDate = DateTime.Today,
                MaturityDate = DateTime.Today.AddDays(-1)
            };
            var validator = new DeltaOneFeedValidator();

            // Act
            var result = validator.Validate(feed);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(ErrorCode.InvalidIdentifier, result.Errors);
            Assert.Contains(ErrorCode.InvalidPrice, result.Errors);
            Assert.Contains(ErrorCode.InvalidIsin, result.Errors);
            Assert.Contains(ErrorCode.InvalidMaturityDate, result.Errors);
        }
    }
}
