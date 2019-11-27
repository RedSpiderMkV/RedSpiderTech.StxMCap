using System.Collections.Generic;
using System.Linq;
using Autofac;
using Serilog;
using StxMCap.DataGrabber.ApiManagement;
using StxMCap.DataGrabber.Factory;
using StxMCap.DataGrabber.FileManagement;
using StxMCap.DataGrabber.Model;
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

            var inputFileParser = _container.Resolve<IInputFileParser>();
            var marketDataRetriever = _container.Resolve<IMarketDataRetriever>();
            var outputDataWriter = _container.Resolve<IOutputDataWriter>();

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
            builder.RegisterType<MarketDataFactory>().As<IMarketDataFactory>().SingleInstance();
            builder.RegisterType<ApiManager>().As<IApiManager>().SingleInstance();
            builder.RegisterType<MarketDataRetriever>().As<IMarketDataRetriever>().SingleInstance();
            builder.RegisterType<OutputDataWriter>().As<IOutputDataWriter>().SingleInstance();
            builder.RegisterType<WebClientWrapperFactory>().As<IWebClientWrapperFactory>();
            builder.RegisterType<AppConfigurationManager>().As<IAppConfigurationManager>();
            builder.RegisterType<InputFileParser>().As<IInputFileParser>();

            _container = builder.Build();
        }
    }
}
