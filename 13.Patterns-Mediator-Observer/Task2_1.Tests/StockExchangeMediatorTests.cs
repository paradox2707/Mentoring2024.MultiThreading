using Task2_1.Services;

namespace Task2_1.Tests
{
    public class StockExchangeMediatorTests
    {
        [Fact]
        public void SellOffer_ShouldNotifyObserversWhenOfferIsMatched()
        {
            // Arrange
            var mediator = new StockExchangeMediator();
            var seller = new RedSocks(mediator);
            var buyer = new Blossomers(mediator);
            mediator.RegisterPlayer(seller);
            mediator.RegisterPlayer(buyer);
            mediator.BuyOffer(buyer.PlayerId, "AAPL", 10);

            // Act
            var result = mediator.SellOffer(seller.PlayerId, "AAPL", 10);

            // Assert
            Assert.True(result);
            Assert.Equal(10, seller.SoldShares);
            Assert.Equal(10, buyer.BoughtShares);
        }

        [Fact]
        public void BuyOffer_ShouldNotifyObserversWhenOfferIsMatched()
        {
            // Arrange
            var mediator = new StockExchangeMediator();
            var seller = new RedSocks(mediator);
            var buyer = new Blossomers(mediator);
            mediator.RegisterPlayer(seller);
            mediator.RegisterPlayer(buyer);
            mediator.SellOffer(seller.PlayerId, "AAPL", 10);

            // Act
            var result = mediator.BuyOffer(buyer.PlayerId, "AAPL", 10);

            // Assert
            Assert.True(result);
            Assert.Equal(10, seller.SoldShares);
            Assert.Equal(10, buyer.BoughtShares);
        }
    }
}