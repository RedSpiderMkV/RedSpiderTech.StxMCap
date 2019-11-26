using StxMCap.DataGrabber.Wrapper;

namespace StxMCap.DataGrabber.Factory
{
    public interface IWebClientWrapperFactory
    {
        IWebClientWrapper GetNewWebClientWrapper();
    }
}