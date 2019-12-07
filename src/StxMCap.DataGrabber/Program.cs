using System.Collections.Generic;
using System.Linq;
using Autofac;
using RedSpiderTech.YahooApi.DataGrabber;
using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;
using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;
using Serilog;
using StxMCap.DataGrabber.FileManagement;
using StxMCap.DataGrabber.Utilities;

namespace StxMCap.DataGrabber
{
    internal class Program
    {
        private static IContainer _container;
        private static ILogger _logger;

        static void Main(string[] args)
        {
            InitialiseContainer();

            var appConfigurationManager = _container.Resolve<IAppConfigurationManager>();
            _logger = _container.Resolve<ILogger>();

            _logger.Information("RedSpiderTech.StxMCap - Data Retrieval");
            _logger.Information("--------------------------------------");

            IInputFileParser inputFileParser = _container.Resolve<IInputFileParser>();
            IMarketDataRetriever marketDataRetriever = _container.Resolve<IYahooMarketDataRetrieverBuilder>().GetMarketDataRetriever();
            IOutputDataWriter outputDataWriter = _container.Resolve<IOutputDataWriter>();

            string[] symbols = inputFileParser.GetInputSymbols();
            IEnumerable<IMarketData> marketDataCollection = symbols.Select(marketDataRetriever.GetMarketData);
            marketDataCollection.ToList().ForEach(outputDataWriter.AppendData);

            outputDataWriter.Dispose();
        }

        private static void InitialiseContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LogInitialiser>().As<ILogInitialiser>().SingleInstance();
            builder.Register(x => x.Resolve<ILogInitialiser>().GetLogger()).As<ILogger>().SingleInstance();
            builder.RegisterType<OutputDataWriter>().As<IOutputDataWriter>().SingleInstance();
            builder.RegisterType<AppConfigurationManager>().As<IAppConfigurationManager>();
            builder.RegisterType<InputFileParser>().As<IInputFileParser>();
            builder.RegisterType<YahooMarketDataRetrieverBuilder>().As<IYahooMarketDataRetrieverBuilder>();

            _container = builder.Build();
        }
    }
}
