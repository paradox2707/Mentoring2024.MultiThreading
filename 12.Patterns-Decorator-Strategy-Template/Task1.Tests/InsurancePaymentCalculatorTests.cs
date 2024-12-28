using Moq;
using Task1.Interfaces;
using Task1.Models;
using Task1.Services;

namespace Task1.Tests;

public class InsurancePaymentCalculatorTests
{
    [Fact]
    public void CalculatePayment_ShouldReturnCorrectPayment_WhenDataIsValid()
    {
        // Arrange
        var mockCurrencyService = new Mock<ICurrencyService>();
        var mockTripRepository = new Mock<ITripRepository>();

        mockCurrencyService.Setup(service => service.LoadCurrencyRate()).Returns(1.2m);
        mockTripRepository.Setup(repo => repo.LoadTrip("John Doe")).Returns(new TripDetails
        {
            TouristName = "John Doe",
            FlyCost = 100m,
            AccomodationCost = 200m,
            ExcursionCost = 50m
        });

        var calculator = new InsurancePaymentCalculator(mockCurrencyService.Object, mockTripRepository.Object);

        // Act
        var payment = calculator.CalculatePayment("John Doe");

        // Assert
        Assert.Equal(420m, payment);
    }

    [Fact]
    public void CalculatePayment_ShouldCallLoadCurrencyRate_Once()
    {
        // Arrange
        var mockCurrencyService = new Mock<ICurrencyService>();
        var mockTripRepository = new Mock<ITripRepository>();

        mockCurrencyService.Setup(service => service.LoadCurrencyRate()).Returns(1.2m);
        mockTripRepository.Setup(repo => repo.LoadTrip("John Doe")).Returns(new TripDetails
        {
            TouristName = "John Doe",
            FlyCost = 100m,
            AccomodationCost = 200m,
            ExcursionCost = 50m
        });

        var calculator = new InsurancePaymentCalculator(mockCurrencyService.Object, mockTripRepository.Object);

        // Act
        calculator.CalculatePayment("John Doe");

        // Assert
        mockCurrencyService.Verify(service => service.LoadCurrencyRate(), Times.Once);
    }

    [Fact]
    public void CalculatePayment_ShouldCallLoadTrip_Once()
    {
        // Arrange
        var mockCurrencyService = new Mock<ICurrencyService>();
        var mockTripRepository = new Mock<ITripRepository>();

        mockCurrencyService.Setup(service => service.LoadCurrencyRate()).Returns(1.2m);
        mockTripRepository.Setup(repo => repo.LoadTrip("John Doe")).Returns(new TripDetails
        {
            TouristName = "John Doe",
            FlyCost = 100m,
            AccomodationCost = 200m,
            ExcursionCost = 50m
        });

        var calculator = new InsurancePaymentCalculator(mockCurrencyService.Object, mockTripRepository.Object);

        // Act
        calculator.CalculatePayment("John Doe");

        // Assert
        mockTripRepository.Verify(repo => repo.LoadTrip("John Doe"), Times.Once);
    }
}
