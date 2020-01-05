using System;

namespace StxMCap.DataGrabber.Model
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
                          DateTime timestamp,
                          string longName,
                          string shortName,
                          string exchangeName,
                          double marketCapital,
                          double currentPrice)
        {
            Symbol = symbol;
            TimeStamp = timestamp;
            LongName = longName;
            ShortName = shortName;
            ExchangeName = exchangeName;
            MarketCapital = marketCapital;
            CurrentPrice = currentPrice;
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
