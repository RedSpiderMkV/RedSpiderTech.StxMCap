using Newtonsoft.Json.Linq;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Factory
{
    public class MarketDataFactory : IMarketDataFactory
    {
        #region Public Methods

        public IMarketData GetMarketDataFromJson(string jsonContent, string symbol)
        {
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
