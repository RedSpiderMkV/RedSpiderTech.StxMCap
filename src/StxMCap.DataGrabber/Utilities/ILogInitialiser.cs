using Serilog;

namespace StxMCap.DataGrabber.Utilities
{
    public interface ILogInitialiser
    {
        ILogger GetLogger();
    }
}