using System;

namespace StxMCap.DataGrabber.Model
{
    public interface IMarketData
    {
        double CurrentPrice { get; }
        string ExchangeName { get; }
        string LongName { get; }
        double MarketCapital { get; }
        string ShortName { get; }
        string Symbol { get; }
        DateTime TimeStamp { get; }
    }
}