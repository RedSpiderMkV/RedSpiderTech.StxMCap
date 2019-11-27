using Serilog;
using StxMCap.DataGrabber.Wrapper;

namespace StxMCap.DataGrabber.Factory
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
