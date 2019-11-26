using System.IO;
using StxMCap.DataGrabber.Utilities;

namespace StxMCap.DataGrabber.FileManagement
{
    public class InputFileParser : IInputFileParser
    {
        #region Private Data

        private readonly string _fileWithPath;

        #endregion

        #region Public Methods

        public InputFileParser(IAppConfigurationManager appConfigurationManager)
        {
            _fileWithPath = appConfigurationManager.InputFile;
        }

        public string[] GetInputSymbols()
        {
            return File.ReadAllLines(_fileWithPath);
        }

        #endregion
    }
}
