using Moq;
using Task1.Interfaces;
using Task1.Services;
using Xunit;

namespace Task1.Tests
{
    public class RoundingCalculatorTests
    {
        [Theory]
        [InlineData(34.56, 35)]
        [InlineData(34.50, 35)]
        [InlineData(34.23, 34)]
        public void CalculatePayment_ShouldRoundPayment(decimal input, decimal expected)
        {
            // Arrange
            var mockInnerCalculator = new Mock<ICalculator>();
            mockInnerCalculator.Setup(c => c.CalculatePayment(It.IsAny<string>())).Returns(input);

            var roundingCalculator = new RoundingCalculator(mockInnerCalculator.Object);

            // Act
            var result = roundingCalculator.CalculatePayment("John Doe");

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
