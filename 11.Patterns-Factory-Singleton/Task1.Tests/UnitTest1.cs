using Task1.Enums;
using Task1.Filters;
using Task1.Models;

namespace Task1.Tests
{
    public class TradeFilterTests
    {
        [Fact]
        public void BofaFilter_ShouldFilterTradesCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Amount = 80 },
                    new Trade { Amount = 60 }
                };

            var filter = new BofaFilter();
            var result = filter.Match(trades);

            Assert.Single(result);
            Assert.Equal(80, result.First().Amount);
        }

        [Fact]
        public void ConnacordFilter_ShouldFilterTradesCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Future", Amount = 20 },
                    new Trade { Type = "Future", Amount = 50 }
                };

            var filter = new ConnacordFilter();
            var result = filter.Match(trades);

            Assert.Single(result);
            Assert.Equal(20, result.First().Amount);
        }

        [Fact]
        public void BarclaysFilter_ShouldFilterTradesCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Option", SubType = "NyOption", Amount = 60 },
                    new Trade { Type = "Option", SubType = "NyOption", Amount = 40 }
                };

            var filter = new BarclaysFilter();
            var result = filter.Match(trades);

            Assert.Single(result);
            Assert.Equal(60, result.First().Amount);
        }

        [Fact]
        public void BarclaysEnglandFilter_ShouldFilterTradesCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Future", Amount = 150 },
                    new Trade { Type = "Future", Amount = 90 }
                };

            var filter = new BarclaysEnglandFilter();
            var result = filter.Match(trades);

            Assert.Single(result);
            Assert.Equal(150, result.First().Amount);
        }

        [Fact]
        public void DeutscheFilter_ShouldFilterTradesCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Option", SubType = "NewOption", Amount = 100 },
                    new Trade { Type = "Option", SubType = "NewOption", Amount = 130 }
                };

            var filter = new DeutscheFilter();
            var result = filter.Match(trades);

            Assert.Single(result);
            Assert.Equal(100, result.First().Amount);
        }

        [Fact]
        public void TradeFilter_ShouldFilterTradesForBankCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Option", SubType = "NyOption", Amount = 60 },
                    new Trade { Type = "Option", SubType = "NyOption", Amount = 40 }
                };

            var filterFactory = new FilterFactory();
            var tradeFilter = new TradeFilter(filterFactory);
            var result = tradeFilter.FilterForBank(trades, Bank.Barclays, Country.USA);

            Assert.Single(result);
            Assert.Equal(60, result.First().Amount);
        }

        [Fact]
        public void TradeFilter_ShouldFilterTradesForBarclaysEnglandCorrectly()
        {
            var trades = new List<Trade>
                {
                    new Trade { Type = "Future", Amount = 150 },
                    new Trade { Type = "Future", Amount = 90 }
                };

            var filterFactory = new FilterFactory();
            var tradeFilter = new TradeFilter(filterFactory);
            var result = tradeFilter.FilterForBank(trades, Bank.Barclays, Country.England);

            Assert.Single(result);
            Assert.Equal(150, result.First().Amount);
        }
    }

}