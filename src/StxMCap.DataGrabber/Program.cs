using System;
using System.Linq;
using Autofac;
using Serilog;
using Serilog.Events;
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

            string logFile = appConfigurationManager.LogFile;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(LogEventLevel.Verbose)
                .WriteTo.File(logFile, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();

            _logger = Log.Logger;

            string header = string.Format("{0,-10} {1,-10} {2,-10}", "Symbol", "MarketCap", "CurrentPrice");
            Console.WriteLine(header);
            Console.WriteLine("".PadLeft(header.Length, '-'));

            var inputFileParser = _container.Resolve<IInputFileParser>();
            string[] symbols = inputFileParser.GetInputSymbols();

            symbols.ToList().ForEach(DisplayMarketData);
        }

        private static void DisplayMarketData(string symbol)
        {
            var apiManager = _container.Resolve<IApiManager>();
            var marketDataFactory = _container.Resolve<IMarketDataFactory>();

            string jsonContent = apiManager.GetJsonData(symbol);
            IMarketData marketData = marketDataFactory.GetMarketDataFromJson(jsonContent, symbol);

            Console.WriteLine(marketData);
        }

        private static void InitialiseContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MarketDataFactory>().As<IMarketDataFactory>().SingleInstance();
            builder.RegisterType<ApiManager>().As<IApiManager>().SingleInstance();
            builder.RegisterType<WebClientWrapperFactory>().As<IWebClientWrapperFactory>();
            builder.RegisterType<AppConfigurationManager>().As<IAppConfigurationManager>();
            builder.RegisterType<InputFileParser>().As<IInputFileParser>();

            _container = builder.Build();
        }
    }
}
