using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;
using Serilog;

namespace RedSpiderTech.YahooApi.DataGrabber.Core.Implementation
{
    public class WebClientWrapperFactory : IWebClientWrapperFactory
    {
        #region Private Data

        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public WebClientWrapperFactory(ILogger logger)
        {
            _logger = logger;

            _logger.Information("WebClientWrapperFactory: Instantiation successful.");
        }

        public IWebClientWrapper GetNewWebClientWrapper()
        {
            _logger.Information("WebClientWrapperFactory: Retrieving new webclient wrapper.");

            return new WebClientWrapper();
        }

        #endregion
    }
}
