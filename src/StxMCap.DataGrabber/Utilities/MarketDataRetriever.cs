using Serilog;
using StxMCap.DataGrabber.ApiManagement;
using StxMCap.DataGrabber.Factory;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Utilities
{
    public class MarketDataRetriever : IMarketDataRetriever
    {
        #region Private Data

        private readonly IApiManager _apiManager;
        private readonly IMarketDataFactory _marketDataFactory;
        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public MarketDataRetriever(ILogger logger, IApiManager apiManager, IMarketDataFactory marketDataFactory)
        {
            _apiManager = apiManager;
            _marketDataFactory = marketDataFactory;
            _logger = logger;

            _logger.Information("MarketDataRetriever: Initialisation successful.");
        }

        public IMarketData GetMarketData(string symbol)
        {
            _logger.Information($"MarketDataRetriever: Retrieving market data from api for symbol: {symbol}");

            string jsonContent = _apiManager.GetJsonData(symbol);
            IMarketData marketData = _marketDataFactory.GetMarketDataFromJson(jsonContent, symbol);

            return marketData;
        }

        #endregion
    }
}
