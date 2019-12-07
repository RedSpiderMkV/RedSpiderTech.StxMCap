using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Interface
{
    public interface IMarketDataFactory
    {
        IMarketData GetMarketDataFromJson(string jsonContent, string symbol);
    }
}