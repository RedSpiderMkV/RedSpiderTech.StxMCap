using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Utilities
{
    public interface IMarketDataRetriever
    {
        IMarketData GetMarketData(string symbol);
    }
}