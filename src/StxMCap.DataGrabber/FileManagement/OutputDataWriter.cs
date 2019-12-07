using System;
using System.IO;
using Serilog;
using StxMCap.DataGrabber.Model;
using StxMCap.DataGrabber.Utilities;

namespace StxMCap.DataGrabber.FileManagement
{
    public class OutputDataWriter : IOutputDataWriter
    {
        #region Private Data

        private readonly string _outputFileWithPath;
        private readonly StreamWriter _streamWriter;
        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public OutputDataWriter(ILogger logger, IAppConfigurationManager appConfigurationManager)
        {
            _logger = logger;

            string fileNameTemplate = appConfigurationManager.OutputFile;
            string filePath = Path.GetDirectoryName(fileNameTemplate);
            string unfixedFileName = Path.GetFileNameWithoutExtension(fileNameTemplate);
            _outputFileWithPath = Path.Combine(filePath, unfixedFileName) + DateTime.UtcNow.Date.ToString("yyyy-MM-dd") + Path.GetExtension(fileNameTemplate);

            _logger.Information($"OutputDataWriter: output file: {_outputFileWithPath}");

            if(File.Exists(_outputFileWithPath))
            {
                _logger.Warning("OutputDataWriter: output file already exists.  Deleting existing file.");
                File.Delete(_outputFileWithPath);
            }

            _streamWriter = File.AppendText(_outputFileWithPath);
            _logger.Information("OutputDataWriter: output file configured for writing.");

            _logger.Information("OutputDataWriter: writing header to file.");
            string header = string.Format("{0,-10} {1,-10} {2,-10}", "Symbol", "MarketCap", "CurrentPrice") + Environment.NewLine;
            _streamWriter.Write(header);
            _streamWriter.WriteLine("".PadLeft(header.Length, '-'));

            _logger.Information("OutputDataWriter: Instantiation successful.");
        }

        public void AppendData(IMarketData marketData)
        {
            _logger.Information($"OutputDataWriter: appending data to file for symbol: {marketData.Symbol}");

            string formattedSymbol = string.Format("{0,-10}", marketData.Symbol);
            string formattedMarketCapital = string.Format("{0:0.00}", marketData.MarketCapital);
            string formattedCurrentPrice = string.Format("{0,-10}", marketData.CurrentPrice);

            _logger.Information("OutputDataWriter: appending data to file.");
            string formattedString = string.Format("{0} {1,-10} {2}", formattedSymbol, formattedMarketCapital, formattedCurrentPrice);
            _streamWriter.WriteLine(formattedString);
        }

        public void Dispose()
        {
            _logger.Information("OutputDataWriter: Cleaning up resources.");
            _streamWriter.Close();
        }

        #endregion
    }
}
