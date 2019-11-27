namespace StxMCap.DataGrabber.Utilities
{
    public interface IAppConfigurationManager
    {
        string InputFile { get; }
        string OutputFile { get; }
        string LogFile { get; }
    }
}