using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using YahooFinanceApi;

namespace StxMCap.DataGrabber.Utilities
{
    public class SecurityDataRetriever : ISecurityDataRetriever
    {
        #region Private Data

        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public SecurityDataRetriever(ILogger logger)
        {
            _logger = logger;

            _logger.Information("SecurityDataRetriever: Instantiated successfully.");
        }

        public IEnumerable<Security> GetSecurityData(string[] symbols)
        {
            _logger.Information("SecurityDataRetriever: Retrieving security data for symbols:");
            foreach (string symbol in symbols)
            {
                _logger.Information($"SecurityDataRetriever: {symbol}");
            }

            Task<IReadOnlyDictionary<string, Security>> retrievalTask = Yahoo.Symbols(symbols)
                .Fields(Field.ShortName, Field.LongName, Field.RegularMarketTime, Field.MarketCap, Field.RegularMarketChange,
                    Field.RegularMarketPrice, Field.RegularMarketChangePercent)
                .QueryAsync();
            retrievalTask.Wait();

            IReadOnlyDictionary<string, Security> retrievedData = retrievalTask.Result;
            return retrievedData.Values;
        }

        #endregion
    }
}
