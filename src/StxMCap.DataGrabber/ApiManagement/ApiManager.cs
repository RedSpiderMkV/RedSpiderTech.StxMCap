using StxMCap.DataGrabber.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StxMCap.DataGrabber.ApiManagement
{
    public class ApiManager : IApiManager
    {
        #region Private Data

        private const string API_URL_TEMPLATE = "https://uk.finance.yahoo.com/quote/{0}?p={0}";
        private readonly IWebClientWrapperFactory _webClientWrapperFactory;

        #endregion

        #region Public Methods

        public ApiManager(IWebClientWrapperFactory webClientWrapperFactory)
        {
            _webClientWrapperFactory = webClientWrapperFactory;
        }

        public string GetJsonData(string symbol)
        {
            string url = string.Format(API_URL_TEMPLATE, symbol);

            var webClient = _webClientWrapperFactory.GetNewWebClientWrapper();
            string pageContent = webClient.DownloadString(url);
            string[] pageContentLines = pageContent.Split('\n');

            string jsonContentHolder = null;
            foreach (string line in pageContentLines)
            {
                if (line.StartsWith("root.App.main"))
                {
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
