using System.Collections.Generic;
using System.Linq;
using Autofac;
using RedSpiderTech.Securities.DataRetriever;
using RedSpiderTech.Securities.DataRetriever.Core;
using RedSpiderTech.Securities.DataRetriever.Model;
using Serilog;
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

            IInputFileParser inputFileParser = _container.Resolve<IInputFileParser>();
            IOutputDataWriter outputDataWriter = _container.Resolve<IOutputDataWriter>();
            ISecurityDataRetriever securityDataRetriever = _container.Resolve<ISecurityDataRetriever>();
            IMarketDataFactory marketDataFactory = _container.Resolve<IMarketDataFactory>();

            string[] symbols = inputFileParser.GetInputSymbols();
            IEnumerable<ISecurityData> securityDataCollection = securityDataRetriever.GetSecurityData(symbols);
            IEnumerable<IMarketData> marketDataCollection = securityDataCollection.Select(marketDataFactory.GetMarketData);
            marketDataCollection.ToList().ForEach(outputDataWriter.AppendData);

            outputDataWriter.Dispose();
        }

        private static void InitialiseContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LogInitialiser>().As<ILogInitialiser>().SingleInstance();
            builder.Register(x => x.Resolve<ILogInitialiser>().GetLogger()).As<ILogger>().SingleInstance();
            builder.RegisterType<SecurityDataRetrieverManager>().As<ISecurityDataRetrieverManager>().SingleInstance();
            builder.RegisterType<OutputDataWriter>().As<IOutputDataWriter>().SingleInstance();
            builder.Register(x => x.Resolve<ISecurityDataRetrieverManager>().GetSecurityDataRetriever()).As<ISecurityDataRetriever>();
            builder.RegisterType<AppConfigurationManager>().As<IAppConfigurationManager>();
            builder.RegisterType<InputFileParser>().As<IInputFileParser>();
            builder.RegisterType<MarketDataFactory>().As<IMarketDataFactory>();

            _container = builder.Build();
        }
    }
}
