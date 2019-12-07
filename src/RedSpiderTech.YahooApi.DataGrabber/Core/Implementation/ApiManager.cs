using System;
using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;
using Serilog;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Implementation
{
    public class ApiManager : IApiManager
    {
        #region Private Data

        private const string API_URL_TEMPLATE = "https://uk.finance.yahoo.com/quote/{0}?p={0}";
        private readonly IWebClientWrapperFactory _webClientWrapperFactory;
        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public ApiManager(ILogger logger, IWebClientWrapperFactory webClientWrapperFactory)
        {
            _webClientWrapperFactory = webClientWrapperFactory;
            _logger = logger;

            _logger.Information("ApiManager: Instantiation successful.");
        }

        public string GetJsonData(string symbol)
        {
            _logger.Information($"ApiManager: Retrieving json data for symbol: {symbol}");

            string url = string.Format(API_URL_TEMPLATE, symbol);
            _logger.Information($"ApiManager: Data URL link: {url}");

            IWebClientWrapper webClient = _webClientWrapperFactory.GetNewWebClientWrapper();

            _logger.Information("ApiManager: Retrieving data from URL link.");
            string pageContent = webClient.DownloadString(url);
            string[] pageContentLines = pageContent.Split('\n');

            string jsonContentHolder = null;
            foreach (string line in pageContentLines)
            {
                if (line.StartsWith("root.App.main"))
                {
                    _logger.Information("ApiManager: root node identified, extracting relevant json data.");
                    jsonContentHolder = line.Split(new string[] { "root.App.main = " }, StringSplitOptions.None)[1];
                    break;
                }
            }

            string jsonContent = jsonContentHolder.Remove(jsonContentHolder.Length - 1);
            return jsonContent;
        }

        #endregion
    }
}
