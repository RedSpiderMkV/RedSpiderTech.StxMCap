using StxMCap.DataGrabber.Wrapper;

namespace StxMCap.DataGrabber.Factory
{
    public class WebClientWrapperFactory : IWebClientWrapperFactory
    {
        #region Public Methods

        public IWebClientWrapper GetNewWebClientWrapper()
        {
            return new WebClientWrapper();
        }

        #endregion
    }
}
