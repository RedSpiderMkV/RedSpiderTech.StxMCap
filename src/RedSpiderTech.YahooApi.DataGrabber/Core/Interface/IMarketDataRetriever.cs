using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Interface
{
    public interface IMarketDataRetriever
    {
        IMarketData GetMarketData(string symbol);
    }
}