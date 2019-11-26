using System.Net;

namespace StxMCap.DataGrabber.Wrapper
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
