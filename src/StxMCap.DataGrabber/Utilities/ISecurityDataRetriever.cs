using System.Collections.Generic;
using YahooFinanceApi;

namespace StxMCap.DataGrabber.Utilities
{
    public interface ISecurityDataRetriever
    {
        IEnumerable<Security> GetSecurityData(string[] symbols);
    }
}