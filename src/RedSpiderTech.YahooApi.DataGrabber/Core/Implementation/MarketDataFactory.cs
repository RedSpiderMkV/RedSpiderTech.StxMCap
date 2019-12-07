using Newtonsoft.Json.Linq;
using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;
using RedSpiderTech.YahooApi.DataGrabber.Model.Implementation;
using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;
using Serilog;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Implementation
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

        public IMarketData GetMarketDataFromJson(string jsonContent, string symbol)
        {
            _logger.Information($"MarketDataFactory: Constructing market data from json content for symbol: {symbol}");

            JObject jsonObject = JObject.Parse(jsonContent);
            JToken dataObject = jsonObject["context"]["dispatcher"]["stores"]["StreamDataStore"]["quoteData"][symbol];

            string symbolValue = dataObject["symbol"].Value<string>();
            string timestamp = dataObject["regularMarketTime"]["raw"].Value<string>();
            string longName = dataObject.Value<string>("longName");
            string shortName = dataObject.Value<string>("shortName");
            string exchangeName = dataObject.Value<string>("fullExchangeName");
            string marketCapital = dataObject["marketCap"]["raw"].Value<string>();
            string currentPrice = dataObject["regularMarketPrice"]["raw"].Value<string>();

            return new MarketData(symbolValue, timestamp, longName, shortName, exchangeName, marketCapital, currentPrice);
        }

        #endregion
    }
}
