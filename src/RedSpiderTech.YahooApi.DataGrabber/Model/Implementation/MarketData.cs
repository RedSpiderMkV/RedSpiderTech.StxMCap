using System;
using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;

namespace RedSpiderTech.YahooApi.DataGrabber.Model.Implementation
{
    public class MarketData : IMarketData
    {
        #region Properties

        public string Symbol { get; }
        public DateTime TimeStamp { get; }
        public string LongName { get; }
        public string ShortName { get; }
        public string ExchangeName { get; }
        public double MarketCapital { get; }
        public double CurrentPrice { get; }

        #endregion

        #region Public Methods

        public MarketData(string symbol,
                          string timestamp,
                          string longName,
                          string shortName,
                          string exchangeName,
                          string marketCapital,
                          string currentPrice)
        {
            Symbol = symbol;
            long unixTimestampSeconds = long.Parse(timestamp);
            TimeStamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds).UtcDateTime;
            LongName = longName;
            ShortName = shortName;
            ExchangeName = exchangeName;

            MarketCapital = double.Parse(marketCapital) / (1 * Math.Pow(10, 9));
            CurrentPrice = double.Parse(currentPrice);
        }

        public override string ToString()
        {
            string formattedSymbol = string.Format("{0,-10}", Symbol);
            string formattedMarketCapital = string.Format("{0:0.00}", MarketCapital);
            string formattedCurrentPrice = string.Format("{0,-10}", CurrentPrice);

            string formattedString = string.Format("{0} {1,-10} {2}", formattedSymbol, formattedMarketCapital, formattedCurrentPrice);
            return formattedString;
        }

        #endregion
    }
}
