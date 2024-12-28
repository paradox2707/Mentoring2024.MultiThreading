using Task1.Models;
using Task1.Consts;
using Xunit;
using Task1.Services;

namespace Task1.Tests
{
    public class ShipmentCalculatorTests
    {
        [Fact]
        public void CalculatePrice_ForFedEx_WithWeightOver300_ShouldApplySurcharge()
        {
            // Arrange
            var order = new Order(ShipmentOptions.FedEx, 350, ProductType.Book);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(5.00d * 1.1, price);
        }

        [Fact]
        public void CalculatePrice_ForFedEx_WithWeight300OrLess_ShouldNotApplySurcharge()
        {
            // Arrange
            var order = new Order(ShipmentOptions.FedEx, 300, ProductType.Book);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(5.00d, price);
        }

        [Fact]
        public void CalculatePrice_ForUPS_WithWeightOver400_ShouldApplySurcharge()
        {
            // Arrange
            var order = new Order(ShipmentOptions.UPS, 450, ProductType.Electronic);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(4.25d * 1.1, price);
        }

        [Fact]
        public void CalculatePrice_ForUPS_WithWeight400OrLess_ShouldNotApplySurcharge()
        {
            // Arrange
            var order = new Order(ShipmentOptions.UPS, 400, ProductType.Electronic);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(4.25d, price);
        }

        [Fact]
        public void CalculatePrice_ForUSPS_WithBook_ShouldApplyDiscount()
        {
            // Arrange
            var order = new Order(ShipmentOptions.USPS, 200, ProductType.Book);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(3.00d * 0.9, price);
        }

        [Fact]
        public void CalculatePrice_ForUSPS_WithNonBook_ShouldNotApplyDiscount()
        {
            // Arrange
            var order = new Order(ShipmentOptions.USPS, 200, ProductType.Electronic);
            var shipmentCalculator = new ShipmentCalculator();

            // Act
            double price = shipmentCalculator.CalculatePrice(order);

            // Assert
            Assert.Equal(3.00d, price);
        }

        [Fact]
        public void CalculatePrice_WithUnknownShipmentOption_ShouldThrowNotImplementedException()
        {
            // Arrange
            var order = new Order((ShipmentOptions)999, 200, ProductType.Electronic);
            var shipmentCalculator = new ShipmentCalculator();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => shipmentCalculator.CalculatePrice(order));
        }
    }
}
