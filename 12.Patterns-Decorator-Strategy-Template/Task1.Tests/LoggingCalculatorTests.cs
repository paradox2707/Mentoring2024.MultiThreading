using Moq;
using Task1.Interfaces;
using Task1.Services;
using Xunit;

namespace Task1.Tests
{
    public class LoggingCalculatorTests
    {
        [Fact]
        public void CalculatePayment_ShouldLogStartAndEnd()
        {
            // Arrange
            var mockInnerCalculator = new Mock<ICalculator>();
            var mockLogger = new Mock<ILogger>();
            mockInnerCalculator.Setup(c => c.CalculatePayment(It.IsAny<string>())).Returns(100m);

            var loggingCalculator = new LoggingCalculator(mockInnerCalculator.Object, mockLogger.Object);

            // Act
            loggingCalculator.CalculatePayment("John Doe");

            // Assert
            mockLogger.Verify(logger => logger.Log("Start"), Times.Once);
            mockLogger.Verify(logger => logger.Log("End"), Times.Once);
        }
    }
}
