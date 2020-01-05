using RedSpiderTech.Securities.DataRetriever.Model;
using Serilog;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Utilities
{
    public class MarketDataFactory : IMarketDataFactory
    {
        #region Private Data

        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public MarketDataFactory(ILogger logger)
        {
            _logger = logger;

            _logger.Information("MarketDataFactory: Instantiation successful.");
        }

        public IMarketData GetMarketData(ISecurityData securityData)
        {
            _logger.Information($"MarketDataFactory: Generating market data for {securityData.Symbol}");

            var marketData = new MarketData(securityData.Symbol,
                                            securityData.TimeStamp,
                                            securityData.LongName,
                                            securityData.ShortName,
                                            securityData.ExchangeName,
                                            securityData.MarketCapital,
                                            securityData.CurrentPrice);

            return marketData;
        }

        #endregion
    }
}
