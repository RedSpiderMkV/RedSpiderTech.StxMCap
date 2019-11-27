using System.IO;
using Serilog;
using StxMCap.DataGrabber.Utilities;

namespace StxMCap.DataGrabber.FileManagement
{
    public class InputFileParser : IInputFileParser
    {
        #region Private Data

        private readonly string _fileWithPath;
        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public InputFileParser(ILogger logger, IAppConfigurationManager appConfigurationManager)
        {
            _fileWithPath = appConfigurationManager.InputFile;
            _logger = logger;

            _logger.Information("InputFileParser: InputFileParser initialised.");
            _logger.Information($"InputFileParser: Input file: {_fileWithPath}");
        }

        public string[] GetInputSymbols()
        {
            _logger.Information("InputFileParser: Retrieving symbols from input file.");
            string[] symbols = File.ReadAllLines(_fileWithPath);

            _logger.Information("InputFileParser: Symbols:");
            foreach (string symbol in symbols)
            {
                _logger.Information("InputFileParser: " + symbol.Trim());
            }

            return symbols;
        }

        #endregion
    }
}
