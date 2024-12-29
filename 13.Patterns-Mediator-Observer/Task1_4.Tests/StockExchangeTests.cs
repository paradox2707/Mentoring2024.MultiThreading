using Task1.Interfaces;
using Task1.Services;

namespace Task1_4.Tests
{
    public class StockExchangeTests
    {
        private readonly IStockExchangeMediator _mediator;
        private readonly RedSocks _redSocks;
        private readonly Blossomers _blossomers;
        private readonly RossStones _rossStones;

        public StockExchangeTests()
        {
            _mediator = new StockExchangeMediator();
            _redSocks = new RedSocks(_mediator);
            _blossomers = new Blossomers(_mediator);
            _rossStones = new RossStones(_mediator);
        }

        [Fact]
        public void TestInitialStatus()
        {
            Assert.Equal(0, _redSocks.SoldShares);
            Assert.Equal(0, _redSocks.BoughtShares);
            Assert.Equal(0, _blossomers.SoldShares);
            Assert.Equal(0, _blossomers.BoughtShares);
            Assert.Equal(0, _rossStones.SoldShares);
            Assert.Equal(0, _rossStones.BoughtShares);
        }

        [Fact]
        public void TestBlossomersBuyOffer_NoMatch()
        {
            _blossomers.BuyOffer("RTC", 2);
            Assert.Equal(0, _blossomers.BoughtShares);
        }

        [Fact]
        public void TestRedSocksSellOffer_MatchWithBlossomers()
        {
            _blossomers.BuyOffer("RTC", 2);
            _redSocks.SellOffer("RTC", 2);
            Assert.Equal(2, _blossomers.BoughtShares);
            Assert.Equal(2, _redSocks.SoldShares);
        }

        [Fact]
        public void TestRossStonesBuyOffer_NoMatch()
        {
            _rossStones.BuyOffer("ABC", 3);
            Assert.Equal(0, _rossStones.BoughtShares);
        }

        [Fact]
        public void TestBlossomersSellOffer_MatchWithRossStones()
        {
            _rossStones.BuyOffer("ABC", 3);
            _blossomers.SellOffer("ABC", 3);
            Assert.Equal(3, _rossStones.BoughtShares);
            Assert.Equal(3, _blossomers.SoldShares);
        }
    }
}