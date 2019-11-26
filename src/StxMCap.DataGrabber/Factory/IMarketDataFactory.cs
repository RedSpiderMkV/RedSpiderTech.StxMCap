using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Factory
{
    public interface IMarketDataFactory
    {
        IMarketData GetMarketDataFromJson(string jsonContent, string symbol);
    }
}