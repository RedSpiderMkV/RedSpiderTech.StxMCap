using System.Net;
using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Implementation
{
    public class WebClientWrapper : IWebClientWrapper
    {
        #region Private Data

        private readonly WebClient _webClient;

        #endregion

        #region Public Methods

        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string DownloadString(string url)
        {
            return _webClient.DownloadString(url);
        }

        #endregion
    }
}
