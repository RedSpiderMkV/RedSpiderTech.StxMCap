namespace StxMCap.DataGrabber.Utilities
{
    public interface IAppConfigurationManager
    {
        string InputFile { get; }
        string LogFile { get; }
    }
}