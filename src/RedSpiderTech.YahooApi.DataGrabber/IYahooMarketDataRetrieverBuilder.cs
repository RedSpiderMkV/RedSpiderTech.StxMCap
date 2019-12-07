using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;

namespace RedSpiderTech.YahooApi.DataGrabber
{
    public interface IYahooMarketDataRetrieverBuilder
    {
        IMarketDataRetriever GetMarketDataRetriever();
    }
}