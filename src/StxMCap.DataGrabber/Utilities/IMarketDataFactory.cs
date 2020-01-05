using RedSpiderTech.Securities.DataRetriever.Model;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.Utilities
{
    public interface IMarketDataFactory
    {
        IMarketData GetMarketData(ISecurityData securityData);
    }
}