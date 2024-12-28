// CalculatorFactoryTests.cs
using Task1.Interfaces;
using Task1.Services;
using Task1.Repos;
using Xunit;
using Moq;

namespace Task1.Tests
{
    public class CalculatorFactoryTests
    {
        private readonly Mock<ICurrencyService> _mockCurrencyService;
        private readonly Mock<ITripRepository> _mockTripRepository;
        private readonly Mock<ILogger> _mockLogger;
        private readonly CalculatorFactory _calculatorFactory;

        public CalculatorFactoryTests()
        {
            _mockCurrencyService = new Mock<ICurrencyService>();
            _mockTripRepository = new Mock<ITripRepository>();
            _mockLogger = new Mock<ILogger>();
            _calculatorFactory = new CalculatorFactory(_mockCurrencyService.Object, _mockTripRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void CreateLoggingCalculator_ShouldReturnLoggingCalculator()
        {
            // Act
            var calculator = _calculatorFactory.CreateLoggingCalculator();

            // Assert
            Assert.IsType<LoggingCalculator>(calculator);
        }

        [Fact]
        public void CreateRoundingCalculator_ShouldReturnRoundingCalculator()
        {
            // Act
            var calculator = _calculatorFactory.CreateRoundingCalculator();

            // Assert
            Assert.IsType<RoundingCalculator>(calculator);
        }

        [Fact]
        public void CreateDynamicCalculator_ShouldReturnCachedLoggingCalculator()
        {
            // Act
            var calculator = _calculatorFactory.CreateDynamicCalculator(
                calc => new CachedCalculator(calc),
                calc => new LoggingCalculator(calc, _mockLogger.Object)
            );

            // Assert
            Assert.IsType<LoggingCalculator>(calculator);
            var innerCalculator = ((LoggingCalculator)calculator).GetInnerCalculator();
            Assert.IsType<CachedCalculator>(innerCalculator);
        }

        [Fact]
        public void CreateDynamicCalculator_ShouldReturnRoundingCachedCalculator()
        {
            // Act
            var calculator = _calculatorFactory.CreateDynamicCalculator(
                calc => new CachedCalculator(calc),
                calc => new RoundingCalculator(calc)
            );

            // Assert
            Assert.IsType<RoundingCalculator>(calculator);
            var innerCalculator = ((RoundingCalculator)calculator).GetInnerCalculator();
            Assert.IsType<CachedCalculator>(innerCalculator);
        }
    }

    // Extension method to get the inner calculator for testing purposes
    public static class CalculatorExtensions
    {
        public static ICalculator GetInnerCalculator(this RoundingCalculator cachedCalculator)
        {
            return (ICalculator)cachedCalculator.GetType()
                .GetField("_innerCalculator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(cachedCalculator);
        }

        public static ICalculator GetInnerCalculator(this LoggingCalculator loggingCalculator)
        {
            return (ICalculator)loggingCalculator.GetType()
                .GetField("_innerCalculator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(loggingCalculator);
        }
    }
}
