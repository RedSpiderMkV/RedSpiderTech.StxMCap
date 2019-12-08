using StxMCap.DataGrabber.Model;
using YahooFinanceApi;

namespace StxMCap.DataGrabber.Utilities
{
    public interface IMarketDataFactory
    {
        IMarketData GetMarketData(Security securityData);
    }
}