﻿using Serilog;
using StxMCap.DataGrabber.Model;
using YahooFinanceApi;

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

        public IMarketData GetMarketData(Security securityData)
        {
            _logger.Information($"MarketDataFactory: Generating market data for {securityData.Symbol}");

            var marketData = new MarketData(securityData.Symbol,
                                            securityData.RegularMarketTime,
                                            securityData.LongName,
                                            securityData.ShortName,
                                            securityData.Exchange,
                                            securityData.MarketCap,
                                            securityData.RegularMarketPrice);

            return marketData;
        }

        #endregion
    }
}
